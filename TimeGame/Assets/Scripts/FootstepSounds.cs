using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    [System.Serializable]
    public class SurfaceSounds
    {
        public string label;
        public PhysicsMaterial physicsMaterial;
        public AudioSource audioSource;
        public AudioClip[] clips;
    }

    [Header("Surface Sound Mappings")]
    public SurfaceSounds[] surfaces;

    [Header("Fallback")]
    public AudioSource defaultAudioSource;
    public AudioClip[] defaultClips;

    [Header("Settings")]
    public float raycastDistance = 1.2f;
    public LayerMask groundLayer = ~0;

    public void PlayFootstep()
    {
        if (TryGetSurfaceForGround(out SurfaceSounds surface))
        {
            PlayFromSurface(surface.audioSource, surface.clips);
        }
        else
        {
            PlayFromSurface(defaultAudioSource, defaultClips);
        }
    }

    private bool TryGetSurfaceForGround(out SurfaceSounds matched)
    {
        matched = null;

        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, groundLayer))
        {
            Collider col = hit.collider;

            if (col.sharedMaterial != null)
            {
                foreach (var surface in surfaces)
                {
                    if (surface.physicsMaterial == col.sharedMaterial)
                    {
                        matched = surface;
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void PlayFromSurface(AudioSource source, AudioClip[] clips)
    {
        if (source == null || clips == null || clips.Length == 0) return;

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        source.PlayOneShot(clip);
    }
}