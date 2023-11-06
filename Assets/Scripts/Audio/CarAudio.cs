using UnityEngine;

namespace Audio
{
    public class CarAudio : MonoBehaviour
    {
        public AudioSource Engine;
        private SpeedCalculator _speed;

        public AudioSource GearChangeSound;
        
        public float PitchOffSet1;
        public float PitchOffSet2;
        public float PitchOffSet3;
        public float PitchOffSet4;
        public float PitchOffSet5;
        public float PitchOffSet6;

        void Start()
        {
            _speed = GetComponent<SpeedCalculator>();
        }

        void Update()
        {
            PitchControl();
            GearChange();
            EngineVolume();
        }


        public void EngineVolume()
        {

            if (Input.GetAxis("Vertical") == 1)
            {
                Engine.volume += Time.deltaTime;
            }
            else
            {
                if (Engine.volume > 0.1f)
                {
                    Engine.volume -= Time.deltaTime;
                }
            }      

        }


        public void GearChange()
        {
            if (_speed.Speed > 30 & _speed.Speed < 31)
            {
                if(GearChangeSound.isPlaying == false)
                {
                    GearChangeSound.Play();
                }
            }

            if (_speed.Speed > 60 & _speed.Speed < 61)
            {
                if (GearChangeSound.isPlaying == false)
                {
                    GearChangeSound.Play();
                }
            }

            if (_speed.Speed > 90 & _speed.Speed < 91)
            {
                if (GearChangeSound.isPlaying == false)
                {
                    GearChangeSound.Play();
                }
            }

            if (_speed.Speed > 120 & _speed.Speed < 121)
            {
                if (GearChangeSound.isPlaying == false)
                {
                    GearChangeSound.Play();
                }
            }

            if (_speed.Speed > 150 & _speed.Speed < 151)
            {
                if (GearChangeSound.isPlaying == false)
                {
                    GearChangeSound.Play();
                }
            }

            if (_speed.Speed > 180 & _speed.Speed < 181)
            {
                if (GearChangeSound.isPlaying == false)
                {
                    GearChangeSound.Play();
                }
            }
        }

        public void PitchControl()
        {
            if (_speed.Speed > 0 & _speed.Speed < 30)
            {
                Engine.pitch = _speed.Speed * PitchOffSet1;
            }

            if (_speed.Speed > 30 & _speed.Speed < 60)
            {
                Engine.pitch = _speed.Speed * PitchOffSet2;
            }

            if (_speed.Speed > 60 & _speed.Speed < 90)
            {
                Engine.pitch = _speed.Speed * PitchOffSet3;
            }

            if (_speed.Speed > 90 & _speed.Speed < 120)
            {
                Engine.pitch = _speed.Speed * PitchOffSet4;
            }

            if (_speed.Speed > 120 & _speed.Speed < 150)
            {
                Engine.pitch = _speed.Speed * PitchOffSet5;
            }

            if (_speed.Speed > 150)
            {
                Engine.pitch = _speed.Speed * PitchOffSet6;
            }
        }


    }
}
