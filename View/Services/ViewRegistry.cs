using System;
using System.Collections.Generic;
using Lab_7.Model.Enums;
using Lab_7.View.Interfaces;

namespace Lab_7.View.Services
{
    public class ViewRegistry(Dictionary<GamePhase, IView> views)
    {
        private readonly Dictionary<GamePhase, IView> _views = views ?? [];

        /// <summary>
        /// Registers a view for a given game phase, overwriting any existing one.
        /// </summary>
        /// <param name="">
        /// GamePhase phase the phase to register a view for.
        /// IView view the view to render for that phase.</param>
        /// <returns></returns>
        public void Register(GamePhase phase, IView view)
        {
            _views[phase] = view;
        }

        /// <summary>
        /// Gets the view registered for the given phase, throws if none is registered.
        /// </summary>
        /// <param name="">GamePhase phase the phase to fetch a view for</param>
        /// <returns></returns>
        public IView Get(GamePhase phase)
        {
            if (_views.TryGetValue(phase, out var view))
            {
                return view;
            }

            throw new InvalidOperationException($"No view registered for phase '{phase}'.");
        }

        /// <summary>
        /// Tries to get the view registered for the given phase, returns false if none is registered.
        /// </summary>
        /// <param name="">GamePhase phase the phase to fetch a view for</param>
        /// <returns></returns>
        public bool TryGet(GamePhase phase, out IView? view)
        {
            return _views.TryGetValue(phase, out view);
        }
    }
}