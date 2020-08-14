using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class HeartIcon : MonoBehaviour
    {
        #region Private

        [SerializeField] private Animator anim;

        private Status status;

        #endregion // Private

        public void SetStatus(Status value)
        {
            status = value;

            anim.SetBool("Full", status == Status.Full);
        }

        public enum Status
        {
            Full,
            Empty
        }
    }
}