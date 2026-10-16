using System;
using System.Collections.Generic;
using Lab_7.Model.Enums;
using Lab_7.View.Interfaces;

namespace Lab_7.View.Services
{
    public class ViewRegistry(Dictionary<GamePhase, IView> views)
    {
        private readonly Dictionary<GamePhase, IView> _views = views ?? [];

        public void Register(GamePhase phase, IView view)
        {
            _views[phase] = view;
        }

        public IView Get(GamePhase phase)
        {
            if (_views.TryGetValue(phase, out var view))
            {
                return view;
            }

            throw new InvalidOperationException($"No view registered for phase '{phase}'.");
        }

        public bool TryGet(GamePhase phase, out IView? view)
        {
            return _views.TryGetValue(phase, out view);
        }
    }
}