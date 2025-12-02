using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace LOLThemes.Wpf.Controls
{
    public enum AnimationType
    {
        Pulse,
        Glow,
        Rotate,
        None
    }

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

        // 动画持续时间
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register(
                nameof(AnimationDuration),
                typeof(Duration),
                typeof(AnimatedBorder),
                new PropertyMetadata(new Duration(TimeSpan.FromSeconds(2)), OnAnimationPropertyChanged));

        public Duration AnimationDuration
        {
            get => (Duration)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        // 动画类型
        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register(
                nameof(AnimationType),
                typeof(AnimationType),
                typeof(AnimatedBorder),
                new PropertyMetadata(AnimationType.None, OnAnimationPropertyChanged));

        public AnimationType AnimationType
        {
            get => (AnimationType)GetValue(AnimationTypeProperty);
            set => SetValue(AnimationTypeProperty, value);
        }

        private static void OnAnimationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AnimatedBorder border && border.IsLoaded)
            {
                border.StartAnimation();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            StartAnimation();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            StopAnimation();
        }

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

        private void StopAnimation()
        {
            if (_animationStoryboard != null)
            {
                _animationStoryboard.Stop(this);
                _animationStoryboard = null;
            }
        }

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
