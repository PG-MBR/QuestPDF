﻿using System;
using QuestPDF.Elements;
using QuestPDF.Infrastructure;

namespace QuestPDF.Fluent
{
    public class RowDescriptor
    {
        internal Row Row { get; } = new();

        public void Spacing(float value)
        {
            Row.Spacing = value;
        }

        private IContainer Item(RowItemType type, float size = 0)
        {
            var element = new RowItem
            {
                Type = type,
                Size = size
            };
            
            Row.Items.Add(element);
            return element;
        }
        
        // TODO: deprecated Box method in QuestPDF 2022.2
        [Obsolete("This element has been renamed. Please use the RelativeItem method.")]
        public IContainer RelativeColumn(float size = 1)
        {
            return Item(RowItemType.Relative, size);
        }
        
        // TODO: deprecated Box method in QuestPDF 2022.2
        [Obsolete("This element has been renamed. Please use the ConstantItem method.")]
        public IContainer ConstantColumn(float size)
        {
            return Item(RowItemType.Constant, size);
        }

        public IContainer RelativeItem(float size = 1)
        {
            return Item(RowItemType.Relative, size);
        }
        
        public IContainer ConstantItem(float size)
        {
            return Item(RowItemType.Constant, size);
        }

        public IContainer AutoItem()
        {
            return Item(RowItemType.Auto);
        }
    }
    
    public static class RowExtensions
    {
        public static void Row(this IContainer element, Action<RowDescriptor> handler)
        {
            var descriptor = new RowDescriptor();
            handler(descriptor);
            element.Element(descriptor.Row);
        }
    }
}