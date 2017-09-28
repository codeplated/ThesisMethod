using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace ThesisMethod.Gestures
{
    public static class Gesture
    {
        public static readonly BindableProperty TappedProperty = BindableProperty.CreateAttached("Tapped", typeof(Command<Point>), typeof(Gesture), null, propertyChanged: CommandChanged);
       
        public static Command<Point> GetCommand(BindableObject view)
        {
            Debug.WriteLine("Get Command Called");
            
            return (Command<Point>)view.GetValue(TappedProperty);
        }

        public static void SetTapped(BindableObject view, Command<Point> value)
        {
            Debug.WriteLine("SetTapped Called");
            view.SetValue(TappedProperty, value);
        }

        private static void CommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Debug.WriteLine("Command changed Called");
            var view = bindable as View;
            if (view != null)
            {
                var effect = GetOrCreateEffect(view);
            }
        }

        private static GestureEffect GetOrCreateEffect(View view)
        {
            Debug.WriteLine("GetOrCreateEffect Called");
            var effect = (GestureEffect)view.Effects.FirstOrDefault(e => e is GestureEffect);
            if (effect == null)
            {
                effect = new GestureEffect();
                view.Effects.Add(effect);
            }
            return effect;
        }

        class GestureEffect : RoutingEffect
        {

            public GestureEffect() : base("ThesisMethod.TapWithPositionGestureEffect")
            {
            }
        }
    }
}
