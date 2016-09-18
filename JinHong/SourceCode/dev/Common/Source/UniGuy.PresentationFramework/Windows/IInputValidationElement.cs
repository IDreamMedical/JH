using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace UniGuy.Windows
{
    public struct InputValidationResult
    {
        public static readonly InputValidationResult Valid = new InputValidationResult { IsValid = true };

        private bool isValid;
        private object invalidContent;
        private object coerceValue;

        public bool IsValid { get { return isValid; } private set { isValid = value; } }
        public object InvalidContent { get { return invalidContent; } private set { invalidContent = value; } }
        //  TODO...Extended..另加的,为了验证失败时提供正常值
        public object CoerceValue { get { return coerceValue; } set { coerceValue = value; } }

        public InputValidationResult(bool isValid, object invalidContent)
        {
            this.isValid = isValid;
            this.invalidContent = invalidContent;
            //  TODO. Remove
            this.coerceValue = null;
        }
    }

    public delegate void InputValidationEventHandler(object sender, InputValidationEventArgs args);

    public class InputValidationEventArgs : RoutedEventArgs
    {
        #region Members
        private object validationData;
        private object validationContext;
        private InputValidationResult validationResult;
        #endregion

        #region Properties

        public object ValidationData
        {
            get { return validationData; }
            private set { validationData = value; }
        }

        public object ValidationContext
        {
            get { return validationContext; }
            private set { validationContext = value; }
        }

        public InputValidationResult ValidationResult
        {
            get { return validationResult; }
            set { validationResult = value; }
        }
        #endregion

        #region Constructors

        public InputValidationEventArgs(object validationData, object validationContext = null)
            : base()
        {
            this.ValidationData = validationData;
            this.ValidationContext = validationContext;
        }

        public InputValidationEventArgs(RoutedEvent routedEvent, object validationData, object validationContext = null)
            : base(routedEvent)
        {
            this.ValidationData = validationData;
            this.ValidationContext = validationContext;
        }

        public InputValidationEventArgs(RoutedEvent routedEvent, object source, object validationData, object validationContext = null)
            : base(routedEvent, source)
        {
            this.ValidationData = validationData;
            this.ValidationContext = validationContext;
        }

        #endregion
    }

    /// <summary>
    /// 这是一个由外部实现验证的输入控件接口
    /// 说明: 一般输入控件值的验证分几层, 1) 绑定的时候只做基本控制,不支持验证上下文 2) 控件内部逻辑, 依赖属性的CoerceValue 3) 最基本的还是绑定数据所在的Model本身的逻辑, 属性setter应该做业务逻辑校验
    ///     如果没有实现DataModel等, 散在的数据之间的验证可以用本方法来实现; TODO: 当然不可能继承所有控件来实现本接口, 所有最好采用Behavior的方式来实现本接口的内容, 更好的复用
    /// 输入控件继承本接口后
    /// 1. 实现protected virtual ValidateInput();
    /* RaiseInputValidationRequestedEvent(Value, ValidationContext);
            RaiseInputValidationCompletedEvent(Value, ValidationContext);*/
    /// 2. 根据业务逻辑在相关需要触发验证的时候(比如LostFocus事件时)调用ValidateInput()
    /// 3. 一般实现方式:
    /// <example>
    /*
        #region Dependency properties
        public static readonly DependencyProperty ValidationContextProperty = DependencyProperty.Register(
            "ValidationContext", typeof(object), typeof(YourInputControl));
        public object ValidationContext
        {
            get { return GetValue(ValidationContextProperty); }
            set { SetValue(ValidationContextProperty, value); }
        }

     * public static readonly DependencyProperty ValidationResultProperty = DependencyProperty.Register(
            "ValidationResult", typeof(InputValidationResult), typeof(NumberEditBox));
        public InputValidationResult ValidationResult
        {
            get { return (InputValidationResult)GetValue(ValidationResultProperty); }
            private set { SetValue(ValidationResultProperty, value); }
        }
        #endregion

        #region Routed events

        public static readonly RoutedEvent InputValidationRequestedEvent = EventManager.RegisterRoutedEvent
            ("InputValidationRequested", RoutingStrategy.Bubble, typeof(InputValidationEventHandler), typeof(YourInputControl));
        public static readonly RoutedEvent InputValidationCompletedEvent = EventManager.RegisterRoutedEvent
            ("InputValidationCompleted", RoutingStrategy.Direct, typeof(InputValidationEventHandler), typeof(YourInputControl));

        public event InputValidationEventHandler InputValidationRequested
        {
            add { AddHandler(InputValidationRequestedEvent, value); }
            remove { RemoveHandler(InputValidationRequestedEvent, value); }
        }

        public event InputValidationEventHandler InputValidationCompleted
        {
            add { AddHandler(InputValidationCompletedEvent, value); }
            remove { RemoveHandler(InputValidationCompletedEvent, value); }
        }

        void RaiseInputValidationRequestedEvent(object validationData, object validationContext = null)
        {
            InputValidationEventArgs ea = new InputValidationEventArgs(InputValidationRequestedEvent, this, validationData, validationContext) { ValidationResult = InputValidationResult.Valid };
            RaiseEvent(ea);
            ValidationResult = ea.ValidationResult;
        }

        void RaiseInputValidationCompletedEvent(object validationData, object validationContext)
        {
            InputValidationEventArgs ea = new InputValidationEventArgs(InputValidationCompletedEvent, this, validationData, validationContext)
                {
                    ValidationResult = this.ValidationResult
                };
            RaiseEvent(ea);
        }

        #endregion
     */
    /// </example>
    /// 注意:其中的路由事件的定义方法,不同控件应该Override此路由事件而不是重复注册;
    /// 4. 由于RaiseInputValidationRequested是Bubble路由事件, 在输入表单中直接对输入容器使用
    /// this.AddHandler(NumberEditBox.InputValidationRequestedEvent, new InputValidationEventHandler(InputValidationRequested));
    /// 5. Xaml中相关输入控件赋值ValidationContext="field1"等, 这是用作简单字段标示, 否则也可以包含复杂的验证上下文数据;
    /// 6. InputValidationRequested应根据ValidationContext实现外部验证逻辑;
    /// 7. 验证完成后应设置ValidationResult; 这是后加的属性, 方便控件根据此属性设置不同错误样式;
    /// </summary>
    public interface IInputValidationElement : IInputElement
    {
        event InputValidationEventHandler InputValidationRequested;
        event InputValidationEventHandler InputValidationCompleted;
        //  验证上下文
        object ValidationContext { get; set; }
        //  验证结果
        InputValidationResult ValidationResult { get; }
    }
}
