﻿using System;
using System.Collections;
using System.Reflection;
using Microsoft.Extensions.CommandLineUtils;

namespace CommandDotNet
{
    public class CommandParameterInfo
    {
        public CommandParameterInfo()
        {
            
        }
        
        public CommandParameterInfo(ParameterInfo parameterInfo)
        {
            ParameterName = parameterInfo.Name;
            ParameterType = parameterInfo.ParameterType;
            CommandOptionType = getCommandOptionType(parameterInfo);
            Required = getIsRequired(parameterInfo);
            DefaultValue = parameterInfo.DefaultValue;
        }
        
        public string ParameterName { get; set; }
        public Type ParameterType { get; set;}
        public CommandOptionType CommandOptionType { get; set;}
        public bool Required { get; set;}
        public object DefaultValue { get; set;}

        private CommandOptionType getCommandOptionType(ParameterInfo parameterInfo)
        {
            if (typeof(IEnumerable).IsAssignableFrom(parameterInfo.ParameterType) && parameterInfo.ParameterType != typeof(string))
            {
                return CommandOptionType.MultipleValue;
            }
                
            if(parameterInfo.ParameterType.IsAssignableFrom(typeof(bool?)))
            {
                return CommandOptionType.NoValue;
            }

            return CommandOptionType.SingleValue;
        }

        private bool getIsRequired(ParameterInfo parameterInfo)
        {
            return parameterInfo.ParameterType.IsValueType
                   && parameterInfo.ParameterType.IsPrimitive
                   && !parameterInfo.HasDefaultValue;
        }
    }
}