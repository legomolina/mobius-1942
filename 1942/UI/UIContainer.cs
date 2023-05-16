using Engine.Core;
using Engine.Core.Input;
using Engine.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.UI
{
    public class UIContainer : IDrawable, IUpdatable
    {
        private List<IInteractable> components = new List<IInteractable>();
        private readonly BatchRenderer renderer;
        private readonly InputManager input;

        public bool Active { get; set; }

        public List<IInteractable> Components => components.ToList();

        public IInteractable? FocusedComponent
        {
            get
            {
                return components.Find(component => component.Focus);
            }
        }

        public int Order { get; set; }

        public UIContainer(BatchRenderer renderer)
        {
            this.input = InputManager.Instance;
            this.renderer = renderer;
        }

        public void Insert(IInteractable component)
        {
            Insert(new IInteractable[] { component });
        }

        public void Insert(IInteractable[] insertComponents)
        {
            Insert(insertComponents.ToList());
        }

        public void Insert(IEnumerable<IInteractable> insertComponents)
        {
            components.AddRange(insertComponents);
        }

        public void LoadContent(AssetManager assetManager) { }

        public void Render()
        {
            foreach (IInteractable component in components)
            {
                if (component.Active)
                {
                    renderer.Render(component);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            GameController? controller = input.GameControllers[0];

            if (controller != null)
            {
                if (controller.IsButtonReleased(GameControllerButtons.DPadDown))
                {
                    if (FocusedComponent == null)
                    {
                        components[0].SetFocus(true);
                    }

                    IEnumerable<IInteractable> nextComponents = components
                        .OrderBy(component => component.TabIndex)
                        .Where(component => component.TabIndex > FocusedComponent!.TabIndex);

                    if (nextComponents.Count() == 0)
                    {
                        if (components.Count > components.IndexOf(FocusedComponent!) + 1)
                        {
                            int index = components.IndexOf(FocusedComponent!) + 1;
                            FocusedComponent!.SetFocus(false);
                            components[index].SetFocus(true);
                        }
                    }
                    else
                    {
                        FocusedComponent!.SetFocus(false);
                        nextComponents.First().SetFocus(true);
                    }
                }

                if (controller.IsButtonReleased(GameControllerButtons.DPadUp))
                {
                    if (FocusedComponent == null)
                    {
                        components[0].SetFocus(true);
                    }

                    IEnumerable<IInteractable> previousComponents = components
                        .OrderBy(component => component.TabIndex)
                        .Where(component => component.TabIndex < FocusedComponent!.TabIndex);

                    if (previousComponents.Count() == 0)
                    {
                        if (components.IndexOf(FocusedComponent!) - 1 >= 0)
                        {
                            int index = components.IndexOf(FocusedComponent!) - 1;
                            FocusedComponent!.SetFocus(false);
                            components[index].SetFocus(true);
                        }
                    }
                    else
                    {
                        FocusedComponent!.SetFocus(false);
                        previousComponents.First().SetFocus(true);
                    }
                }
            }

            foreach (IInteractable component in components)
            {
                if (component.Active)
                {
                    component.Update(gameTime);
                }
            }
        }
    }
}
