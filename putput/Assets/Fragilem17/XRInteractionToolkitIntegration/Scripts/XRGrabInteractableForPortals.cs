using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Fragilem17.MirrorsAndPortals
{
    public class XRGrabInteractableForPortals : XRGrabInteractable
    {
		private bool _usedFromUpdateTargetPos = false;
		public PortalableObject MyPortalableObject;

		protected override void OnEnable()
		{
			base.OnEnable();

			if (MyPortalableObject == null)
			{
				MyPortalableObject = GetComponent<PortalableObject>();
			}
		}

		public void UpdateTargetPos() {
			_usedFromUpdateTargetPos = true;
			Grab();
			_usedFromUpdateTargetPos = false;
        }

		// Grab is the only method that reset the pose position, however it calls SetupRigidbodyGrab
		protected override void Grab()
		{
			base.Grab();
		}


		// this base method remembers the RB settings, but is now being called twice due to "UpdateTargetPos", re-remebering already changed settings.
		protected override void SetupRigidbodyGrab(Rigidbody rigidbody)
		{
			if (_usedFromUpdateTargetPos)
			{
				return;
			}

			base.SetupRigidbodyGrab(rigidbody);

		}

		protected override void OnSelectEntered(SelectEnterEventArgs args)
		{
			base.OnSelectEntered(args);

			if (MyPortalableObject != null)
			{
				MyPortalableObject.EnablePortalAlongWithMasterPortalable = true;
			}
		}

		protected override void OnSelectExited(SelectExitEventArgs args)
		{
			base.OnSelectExited(args);

			if (MyPortalableObject != null)
			{
				MyPortalableObject.EnablePortalAlongWithMasterPortalable = false;
			}
		}
	}
}
