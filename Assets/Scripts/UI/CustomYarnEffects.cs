using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
namespace Yarn.Unity
{

    //this class is just a slightly modified version of the Yarnspinner Effect class.
    //I'm mainly resuing code from the InterruptableWait and the CoroutineInterruptToken class to Modify their TypeWRitterEffect
    public static class CustomYarnEffects
    {
        public class CoroutineInterruptToken
        {

            /// <summary>
            /// The state that the token is in.
            /// </summary>
            enum State
            {
                NotRunning,
                Running,
                Interrupted,
            }
            private State state = State.NotRunning;

            public bool CanInterrupt => state == State.Running;
            public bool WasInterrupted => state == State.Interrupted;
            public void Start() => state = State.Running;
            public void Interrupt()
            {
                if (CanInterrupt == false)
                {
                    throw new InvalidOperationException($"Cannot stop {nameof(CoroutineInterruptToken)}; state is {state} (and not {nameof(State.Running)}");
                }
                state = State.Interrupted;
            }

            public void Complete() => state = State.NotRunning;
        }

        private static IEnumerator InterruptableWait(float duration, CoroutineInterruptToken stopToken = null)
        {
            float accumulator = 0;
            while (accumulator < duration)
            {
                if (stopToken?.WasInterrupted ?? false)
                {
                    yield break;
                }

                yield return null;
                accumulator += Time.deltaTime;
            }
        }

        public static IEnumerator CustomizableTypewriter(TextMeshProUGUI text, float lettersPerSecond, Action onCharacterTyped, Action onPauseStarted, Action onPauseEnded, Action OnTextComplete, Stack<(int position, float duration)> pausePositions, CoroutineInterruptToken stopToken = null)
        {
            stopToken?.Start();

            // Start with everything invisible
            text.maxVisibleCharacters = 0;

            // Wait a single frame to let the text component process its
            // content, otherwise text.textInfo.characterCount won't be
            // accurate
            yield return null;

            // How many visible characters are present in the text?
            var characterCount = text.textInfo.characterCount;

            // Early out if letter speed is zero, text length is zero
            if (lettersPerSecond <= 0 || characterCount == 0)
            {
                // Show everything and return
                text.maxVisibleCharacters = characterCount;
                stopToken?.Complete();
                yield break;
            }

            // Convert 'letters per second' into its inverse
            float secondsPerLetter = 1.0f / lettersPerSecond;

            // If lettersPerSecond is larger than the average framerate, we
            // need to show more than one letter per frame, so simply
            // adding 1 letter every secondsPerLetter won't be good enough
            // (we'd cap out at 1 letter per frame, which could be slower
            // than the user requested.)
            //
            // Instead, we'll accumulate time every frame, and display as
            // many letters in that frame as we need to in order to achieve
            // the requested speed.
            var accumulator = Time.deltaTime;

            while (text.maxVisibleCharacters < characterCount)
            {
                if (stopToken?.WasInterrupted ?? false)
                {
                    OnTextComplete?.Invoke();
                    yield break;
                }

                // We need to show as many letters as we have accumulated
                // time for.
                while (accumulator >= secondsPerLetter)
                {
                    text.maxVisibleCharacters += 1;

                    // ok so the change needs to be that if at any point we hit the pause position
                    // we instead stop worrying about letters
                    // and instead we do a normal wait for the necessary duration
                    if (pausePositions != null && pausePositions.Count != 0)
                    {
                        if (text.maxVisibleCharacters == pausePositions.Peek().Item1)
                        {
                            var pause = pausePositions.Pop();
                            onPauseStarted?.Invoke();
                            yield return CustomYarnEffects.InterruptableWait(pause.Item2, stopToken);
                            onPauseEnded?.Invoke();

                            // need to reset the accumulator
                            accumulator = Time.deltaTime;
                        }
                    }

                    onCharacterTyped?.Invoke();
                    accumulator -= secondsPerLetter;
                }
                accumulator += Time.deltaTime;

                yield return null;
            }
            OnTextComplete?.Invoke();
            // We either finished displaying everything, or were
            // interrupted. Either way, display everything now.
            text.maxVisibleCharacters = characterCount;


            stopToken?.Complete();
        }
    }
}
