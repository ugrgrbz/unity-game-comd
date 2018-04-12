using System.Collections; // For IEnumerator
using UnityEngine; // For MonoBehaviour, WWWForm, WWW
using UnityEngine.SceneManagement;

public class USER: MonoBehaviour
{

    // PHP Links
    protected readonly string register_url = "prodian.site/WFK/wfk_register.php";
    protected readonly string login_url = "prodian.site/WFK/wfk_login.php";
    protected readonly string register_check_url = "prodian.site/WFK/wfk_registernamecheck.php";
    protected readonly string save_score_url = "prodian.site/WFK/wfk_uploadscore.php";
    protected readonly string highscores_url = "prodian.site/WFK/wfk_highscores.php";

    // Parameters
    protected readonly int reconnect_time = 10; // if cannot save score to db, try again after 10 seconds;

    // Scenes
    protected readonly string main_scene_name = "The_Viking_Village";
    protected readonly string login_scene_name = "Main Scene";

    // Variables
    protected static string USERNAME = "";
    protected static int USERID = 0;
    protected static int SCORE = 0;     //players high score

    // Methods 
    protected void save_score(int newScore)
    {
        SCORE = newScore;
        PlayerPrefs.SetInt("SCORE", SCORE);
        PlayerPrefs.Save();

        StartCoroutine(save_score_to_database());
    }

    IEnumerator save_score_to_database()
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", USERID);
        form.AddField("hs", SCORE);

        WWW updateScorephp = new WWW(save_score_url, form);
        yield return updateScorephp;

        if (!updateScorephp.Equals("1"))
        {
            yield return new WaitForSecondsRealtime(reconnect_time);
            // Not: WaitForSeconds: Oyun pause olunca yani Time.timeScale = 0; olunca duracaktır.
            // Ama bu fonksiyon oyundan bağımsız olduğu için WaitForSecondsRealtime methodu kullanılmalıdır.
            StartCoroutine(save_score_to_database());
        }
    }

    protected void LOGOUT()
    {
        // Re-Save when logout 
        StartCoroutine(save_score_to_database());

        PlayerPrefs.SetInt("USER_ID", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(login_scene_name);
    }
}