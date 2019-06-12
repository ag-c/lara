﻿/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Integrative.Lara.Tests.DOM
{
    public class ElementAttributes
    {
        int _counter;

        [Fact]
        public void ElementProperties()
        {
            TestElement<Anchor>("a");
            TestElement<Button>("button");
            TestElement<ColGroup>("colgroup");
            TestElement<Image>("img");
            TestElement<Input>("input");
            TestElement<Label>("label");
            TestElement<Link>("link");
            TestElement<ListItem>("li");
            TestElement<Meta>("meta");
            TestElement<Meter>("meter");
            TestElement<Option>("option");
            TestElement<OptionGroup>("optgroup");
            TestElement<OrderedList>("ol");
            TestElement<Script>("script");
            TestElement<Select>("select");
            TestElement<Table>("table");
            TestElement<TableCell>("td");
            TestElement<TableHeader>("th");
            TestElement<TextArea>("textarea");
        }

        [Fact]
        public void ElementNeedsTag()
        {
            DomOperationsTesting.Throws<ArgumentNullException>(() => Element.Create(""));
            DomOperationsTesting.Throws<ArgumentNullException>(() => Element.Create(null));
        }

        private void TestElement<T>(string tagName) where T : Element
        {
            var instance = (Element)Activator.CreateInstance(typeof(T));
            Assert.Equal(tagName, instance.TagName);
            TestProperties(instance);
        }

        private void TestProperties(Element instance)
        {
            var type = instance.GetType();
            foreach (var property in type.GetProperties())
            {
                if (property.SetMethod != null)
                {
                    TestProperty(instance, property);
                }
            }
        }

        private void TestProperty(Element instance, PropertyInfo property)
        {
            var type = property.PropertyType;
            if (!GetTestValue(type, out var value))
            {
                return;
            }
            property.SetValue(instance, value);
            var result = property.GetValue(instance);
            Assert.Equal(value, result);
        }

        private bool GetTestValue(Type type, out object value)
        {
            _counter++;
            if (type == typeof(bool))
            {
                value = true;
            }
            else if (type == typeof(int))
            {
                value = _counter;
            }
            else if (type == typeof(string))
            {
                value = _counter.ToString();
            }
            else
            {
                value = null;
                return false;
            }
            return true;
        }

        [Fact]
        public void GetChildPositionNotFound()
        {
            var a = Element.Create("div");
            var b = Element.Create("div");
            int index = b.GetChildElementPosition(a);
            Assert.Equal(-1, index);
        }

        [Fact]
        public void ElementDescendsFromItself()
        {
            var x = Element.Create("div");
            Assert.True(x.DescendsFrom(x));
        }

        [Fact]
        public void SetIntAttribute()
        {
            var input = new Input
            {
                Height = 10
            };
            Assert.Equal(10, input.Height);
            input.Height = null;
            Assert.Null(input.Height);
        }

        [Fact]
        public async void ElementOnOptions()
        {
            bool executed = false;
            var div = Element.Create("div");
            div.On(new EventSettings
            {
                EventName = "click",
                Handler = app =>
                {
                    executed = true;
                    return Task.CompletedTask;
                }
            });
            var context = new Mock<IPageContext>();
            await div.NotifyEvent("click", context.Object);
            Assert.True(executed);
        }

        [Fact]
        public void RemoveClassRemovesClass()
        {
            var button = new Button
            {
                Class = "red blue green"
            };
            button.RemoveClass("blue");
            Assert.Equal("red green", button.Class);
        }

        [Fact]
        public void AddClassAddsClass()
        {
            var button = new Button();
            button.AddClass("red");
            Assert.Equal("red", button.Class);
        }

        [Fact]
        public void SetFlagAttributes()
        {
            var button = new Button();
            button.SetFlagAttribute("hidden", true);
            Assert.True(button.Hidden);
        }

        [Fact]
        public void InputAttributes()
        {
            var input = new Input
            {
                MaxLength = 5,
                Size = 3,
                Width = 11
            };
            Assert.Equal(5, input.MaxLength);
            Assert.Equal(3, input.Size);
            Assert.Equal(11, input.Width);
        }

        [Fact]
        public void EncodeTextNode()
        {
            var n1 = new TextNode("&lt;");
            var n2 = new TextNode("&lt;", false);
            Assert.Equal("&amp;lt;", n1.Data);
            Assert.Equal("&lt;", n2.Data);
        }

        [Fact]
        public void ImageProperties()
        {
            var image = new Image
            {
                Height = 1,
                Width = 2
            };
            Assert.Equal(1, image.Height);
            Assert.Equal(2, image.Width);
        }

        [Fact]
        public void OrderedListAttributes()
        {
            var ol = new OrderedList
            {
                Start = 1
            };
            Assert.Equal(1, ol.Start);
        }

        [Fact]
        public void TextAreaProperties()
        {
            var x = new TextArea
            {
                Cols = 1,
                MaxLength = 2,
                Rows = 3
            };
            Assert.Equal(1, x.Cols);
            Assert.Equal(2, x.MaxLength);
            Assert.Equal(3, x.Rows);
        }

        [Fact]
        public void NotifyValueTextArea()
        {
            var x = new TextArea();
            x.NotifyValue(new Lara.Delta.ElementEventValue
            {
                Value = "lala"
            });
            Assert.Equal("lala", x.Value);
        }

        [Fact]
        public void TableHeaderProperties()
        {
            var x = new TableHeader
            {
                ColSpan = 1,
                RowSpan = 2
            };
            Assert.Equal(1, x.ColSpan);
            Assert.Equal(2, x.RowSpan);
        }

        [Fact]
        public void EventSettingsAttributes()
        {
            var x = new EventSettings
            {
                Block = true,
                BlockElementId = "aaa",
                BlockHtmlMessage = "baa"
            };
            Assert.True(x.Block);
            Assert.Equal("aaa", x.BlockElementId);
            Assert.Equal("baa", x.BlockHtmlMessage);
        }

        [Fact]
        public void LaraOptionsProperties()
        {
            var x = new LaraOptions
            {
                AllowLocalhostOnly = false,
                ShowNotFoundPage = false,
                Port = 1234
            };
            Assert.False(x.AllowLocalhostOnly);
            Assert.False(x.ShowNotFoundPage);
            Assert.Equal(1234, x.Port);
        }

        [Fact]
        public void DuplicateElementEmptyConstructor()
        {
            var instance = Activator.CreateInstance<DuplicateElementId>();
            Assert.NotNull(instance);
        }

        [Fact]
        public void DuplicateElementInner()
        {
            var inner = new InvalidOperationException("lala");
            var instance = new DuplicateElementId("lele", inner);
            Assert.Same(inner, instance.InnerException);
            Assert.Equal("lele", instance.Message);
        }

        [Fact]
        public void TableCellProperties()
        {
            var x = new TableCell
            {
                ColSpan = 1,
                RowSpan = 2
            };
            Assert.Equal(1, x.ColSpan);
            Assert.Equal(2, x.RowSpan);
        }

        [Fact]
        public void ColGroupProperties()
        {
            var x = new ColGroup
            {
                Span = 1
            };
            Assert.Equal(1, x.Span);
        }

        [Fact]
        public void LoopSelectOptions()
        {
            var select = new Select();
            var option1 = new Option();
            var group = Element.Create("optgroup");
            var option2 = new Option();
            group.AppendChild(option2);
            select.AppendChild(option1);
            select.AppendChild(group);
            Assert.Equal( new List<Option>{ option1, option2 }, select.Options);
        }

        [Fact]
        public void SelectProperties()
        {
            var select = new Select
            {
                Size = 3
            };
            Assert.Equal(3, select.Size);
        }

        [Fact]
        public void SelectNotifyValue()
        {
            var select = new Select();
            select.NotifyValue(new Lara.Delta.ElementEventValue
            {
                Value = "lala"
            });
            Assert.Equal("lala", select.Value);
        }

        [Fact]
        public void SelectAddOption()
        {
            var x = new Select();
            x.AddOption("myvalue", "this is the text");
            var option = x.Options.FirstOrDefault();
            Assert.NotNull(option);
            Assert.Equal("myvalue", option.Value);
            var text = option.Children.FirstOrDefault() as TextNode;
            Assert.NotNull(text);
            Assert.Equal("this is the text", text.Data);
        }

        [Fact]
        public void OptionWithValueGetsSelected()
        {
            var select = new Select
            {
                Value = "lolo"
            };
            var option = new Option
            {
                Value = "lolo"
            };
            select.AppendChild(option);
            Assert.True(option.Selected);
        }

        [Fact]
        public void AddGroupWithSelectedOption()
        {
            var select = new Select
            {
                Value = "lolo"
            };
            var option = new Option
            {
                Value = "lolo"
            };
            var group = new OptionGroup();
            group.AppendChild(option);
            select.AppendChild(group);
            Assert.True(option.Selected);
        }

        [Fact]
        public void AddSelectedOptionInGroup()
        {
            var select = new Select
            {
                Value = "lolo"
            };
            var option = new Option
            {
                Value = "lolo"
            };
            var group = new OptionGroup();
            select.AppendChild(group);
            group.AppendChild(option);
            Assert.True(option.Selected);
        }
    }
}
