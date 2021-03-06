﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Detached.EntityFramework.Plugins
{
    public class PluginManager : IPluginManager
    {
        List<IDetachedPlugin> _plugins;

        public PluginManager(IServiceProvider _serviceProvider)
        {
            _plugins = _serviceProvider.GetServices<IDetachedPlugin>()
                                       .OrderByDescending(p => p.Priority)
                                       .ToList();
        }

        public void Initialize()
        {
            foreach (IDetachedPlugin plugin in _plugins)
                plugin.Initialize();
        }

        public void Enable()
        {
            foreach (IDetachedPlugin plugin in _plugins)
                plugin.IsEnabled = true;
        }

        public void Disable()
        {
            foreach (IDetachedPlugin plugin in _plugins)
                plugin.IsEnabled = false;
        }

        public IDetachedPlugin this[string name]
        {
            get
            {
                foreach (IDetachedPlugin plugin in _plugins)
                {
                    if (string.Equals(plugin.Name, name, StringComparison.CurrentCultureIgnoreCase))
                        return plugin; 
                }
                return null;
            }
        }
  
        public TPlugin Get<TPlugin>() 
            where TPlugin : class
        {
            Type type = typeof(TPlugin);
            foreach (IDetachedPlugin plugin in _plugins)
            {
                if (plugin.GetType() == type)
                    return (TPlugin)plugin;
            }
            return null;
        }

        public void Dispose()
        {
            foreach (IDetachedPlugin plugin in _plugins)
            {
                plugin.Dispose();
            }
        }
    }
}