// GENERATED AUTOMATICALLY FROM 'Assets/_InputControls/DialogueInputControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DialogueInputControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DialogueInputControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DialogueInputControl"",
    ""maps"": [
        {
            ""name"": ""Dialogue"",
            ""id"": ""6f8857ca-63cc-4391-a301-774c2453aa87"",
            ""actions"": [
                {
                    ""name"": ""Next"",
                    ""type"": ""Button"",
                    ""id"": ""ee38abbf-b709-4269-bf1f-53a95d91728e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartTalk"",
                    ""type"": ""Button"",
                    ""id"": ""24f89af9-a6b6-4093-ba69-06c9cb7e5fa5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8ab566cc-0b7a-41f1-8dc9-b15652e46c7a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eed8ea96-377a-4b9b-8a0d-efdf8937d228"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartTalk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Dialogue
        m_Dialogue = asset.FindActionMap("Dialogue", throwIfNotFound: true);
        m_Dialogue_Next = m_Dialogue.FindAction("Next", throwIfNotFound: true);
        m_Dialogue_StartTalk = m_Dialogue.FindAction("StartTalk", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Dialogue
    private readonly InputActionMap m_Dialogue;
    private IDialogueActions m_DialogueActionsCallbackInterface;
    private readonly InputAction m_Dialogue_Next;
    private readonly InputAction m_Dialogue_StartTalk;
    public struct DialogueActions
    {
        private @DialogueInputControl m_Wrapper;
        public DialogueActions(@DialogueInputControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Next => m_Wrapper.m_Dialogue_Next;
        public InputAction @StartTalk => m_Wrapper.m_Dialogue_StartTalk;
        public InputActionMap Get() { return m_Wrapper.m_Dialogue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogueActions set) { return set.Get(); }
        public void SetCallbacks(IDialogueActions instance)
        {
            if (m_Wrapper.m_DialogueActionsCallbackInterface != null)
            {
                @Next.started -= m_Wrapper.m_DialogueActionsCallbackInterface.OnNext;
                @Next.performed -= m_Wrapper.m_DialogueActionsCallbackInterface.OnNext;
                @Next.canceled -= m_Wrapper.m_DialogueActionsCallbackInterface.OnNext;
                @StartTalk.started -= m_Wrapper.m_DialogueActionsCallbackInterface.OnStartTalk;
                @StartTalk.performed -= m_Wrapper.m_DialogueActionsCallbackInterface.OnStartTalk;
                @StartTalk.canceled -= m_Wrapper.m_DialogueActionsCallbackInterface.OnStartTalk;
            }
            m_Wrapper.m_DialogueActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Next.started += instance.OnNext;
                @Next.performed += instance.OnNext;
                @Next.canceled += instance.OnNext;
                @StartTalk.started += instance.OnStartTalk;
                @StartTalk.performed += instance.OnStartTalk;
                @StartTalk.canceled += instance.OnStartTalk;
            }
        }
    }
    public DialogueActions @Dialogue => new DialogueActions(this);
    public interface IDialogueActions
    {
        void OnNext(InputAction.CallbackContext context);
        void OnStartTalk(InputAction.CallbackContext context);
    }
}
