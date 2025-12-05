using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace LOLThemes.Wpf.Controls
{
    /// <summary>
    /// 动画类型枚举。
    /// </summary>
    public enum AnimationType
    {
        /// <summary>
        /// 脉冲动画：缩放效果
        /// </summary>
        Pulse,
        /// <summary>
        /// 发光动画：模糊半径变化
        /// </summary>
        Glow,
        /// <summary>
        /// 旋转动画：360度旋转
        /// </summary>
        Rotate,
        /// <summary>
        /// 无动画
        /// </summary>
        None
    }

    /// <summary>
    /// 动画边框控件，支持多种动画效果。
    /// 可以显示脉冲、发光或旋转动画，常用于吸引用户注意。
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;controls:AnimatedBorder 
    ///     AnimationType="Pulse"
    ///     AnimationDuration="0:0:2"
    ///     BorderBrush="#C8AA6E"
    ///     BorderThickness="2"&gt;
    ///     &lt;TextBlock Text="动画内容"/&gt;
    /// &lt;/controls:AnimatedBorder&gt;
    /// </code>
    /// </example>
    public class AnimatedBorder : Border
    {
        private Storyboard? _animationStoryboard;

        static AnimatedBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(AnimatedBorder),
                new FrameworkPropertyMetadata(typeof(AnimatedBorder)));
        }

        public AnimatedBorder()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        /// <summary>
        /// 标识 <see cref="AnimationDuration"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(
                nameof(AnimationDuration),
                typeof(Duration),
                typeof(AnimatedBorder),
                new PropertyMetadata(new Duration(TimeSpan.FromSeconds(2)), OnAnimationPropertyChanged));

        /// <summary>
        /// 获取或设置动画持续时间。
        /// 默认值为 2 秒。
        /// </summary>
        public Duration AnimationDuration
        {
            get => (Duration)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        /// <summary>
        /// 标识 <see cref="AnimationType"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register(
                nameof(AnimationType),
                typeof(AnimationType),
                typeof(AnimatedBorder),
                new PropertyMetadata(AnimationType.None, OnAnimationPropertyChanged));

        /// <summary>
        /// 获取或设置动画类型。
        /// 默认值为 <see cref="AnimationType.None"/>（无动画）。
        /// </summary>
        public AnimationType AnimationType
        {
            get => (AnimationType)GetValue(AnimationTypeProperty);
            set => SetValue(AnimationTypeProperty, value);
        }

        /// <summary>
        /// 当动画相关属性值改变时调用。
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="e">属性变更事件参数</param>
        private static void OnAnimationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AnimatedBorder border && border.IsLoaded)
            {
                border.StartAnimation();
            }
        }

        /// <summary>
        /// 控件加载时启动动画。
        /// </summary>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            StartAnimation();
        }

        /// <summary>
        /// 控件卸载时停止动画。
        /// </summary>
        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            StopAnimation();
        }

        /// <summary>
        /// 启动动画。如果动画类型为 None，则不启动。
        /// </summary>
        private void StartAnimation()
        {
            StopAnimation();

            if (AnimationType == AnimationType.None)
                return;

            _animationStoryboard = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever
            };

            switch (AnimationType)
            {
                case AnimationType.Pulse:
                    CreatePulseAnimation();
                    break;
                case AnimationType.Glow:
                    CreateGlowAnimation();
                    break;
                case AnimationType.Rotate:
                    CreateRotateAnimation();
                    break;
            }

            _animationStoryboard?.Begin(this, true);
        }

        /// <summary>
        /// 停止当前动画。
        /// </summary>
        private void StopAnimation()
        {
            if (_animationStoryboard != null)
            {
                _animationStoryboard.Stop(this);
                _animationStoryboard = null;
            }
        }

        /// <summary>
        /// 创建脉冲动画（缩放效果）。
        /// </summary>
        private void CreatePulseAnimation()
        {
            // 缩放动画
            RenderTransformOrigin = new Point(0.5, 0.5);
            RenderTransform = new ScaleTransform(1, 1);

            var scaleXAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.05,
                Duration = AnimationDuration,
                AutoReverse = true,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };

            var scaleYAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.05,
                Duration = AnimationDuration,
                AutoReverse = true,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };

            Storyboard.SetTarget(scaleXAnimation, this);
            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(scaleYAnimation, this);
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));

            _animationStoryboard.Children.Add(scaleXAnimation);
            _animationStoryboard.Children.Add(scaleYAnimation);
        }

        /// <summary>
        /// 创建发光动画（模糊半径变化）。
        /// </summary>
        private void CreateGlowAnimation()
        {
            // 发光效果动画
            if (Effect == null)
            {
                Effect = new DropShadowEffect
                {
                    Color = Colors.Gold,
                    BlurRadius = 0,
                    ShadowDepth = 0,
                    Opacity = 0.8
                };
            }

            if (Effect is DropShadowEffect dropShadow)
            {
                var blurAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 30,
                    Duration = AnimationDuration,
                    AutoReverse = true,
                    EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
                };

                Storyboard.SetTarget(blurAnimation, this);
                Storyboard.SetTargetProperty(blurAnimation, new PropertyPath("Effect.BlurRadius"));

                _animationStoryboard.Children.Add(blurAnimation);
            }
        }

        /// <summary>
        /// 创建旋转动画（360度旋转）。
        /// </summary>
        private void CreateRotateAnimation()
        {
            // 旋转动画
            RenderTransformOrigin = new Point(0.5, 0.5);
            RenderTransform = new RotateTransform(0);

            var rotateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = AnimationDuration
            };

            Storyboard.SetTarget(rotateAnimation, this);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("RenderTransform.Angle"));

            _animationStoryboard.Children.Add(rotateAnimation);
        }
    }
}
