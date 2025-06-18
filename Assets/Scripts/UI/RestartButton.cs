using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartButton : UIButton
    {
        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}