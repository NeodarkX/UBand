package pe.com.uband.uband;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Patterns;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ScrollView;
import android.widget.TextView;
import android.widget.Toast;


public class MainActivity extends AppCompatActivity {

    private static final String TAG = "MainActivity";
    private static final int REQUEST_SIGNUP = 0;
    EditText _emailText;
    EditText _passwordText;
    Button _loginButton;
    TextView _signupText;
    ScrollView _scwMain;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        _emailText = (EditText) findViewById(R.id.input_email);
        _passwordText = (EditText) findViewById(R.id.input_password);
        _loginButton = (Button) findViewById(R.id.btn_login);
        _signupText = (TextView) findViewById(R.id.link_signup);
        _scwMain = (ScrollView) findViewById(R.id.scrollViewMain);
        _scwMain.setVerticalScrollBarEnabled(false);
        _scwMain.setHorizontalScrollBarEnabled(false);
        _loginButton.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                //login();
                startActivity(new Intent(MainActivity.this, PrincipalActivity.class));
            }
        });

        _signupText.setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(),SignUpActivity.class);
                startActivityForResult(intent,REQUEST_SIGNUP);
            }
        });


    }

    public void login(){

        if(!validate()){
            onLoginFailed();
            return;
        }

        _loginButton.setEnabled(false);

        final ProgressDialog progressDialog = new ProgressDialog(MainActivity.this,R.style.AppTheme_Dark_Dialog);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage("Autenticando...");
        progressDialog.show();

        String email = _emailText.getText().toString();
        String password = _passwordText.getText().toString();

        new android.os.Handler().postDelayed(
                new Runnable() {
                    @Override
                    public void run() {
                        onLoginSucess();
                        progressDialog.dismiss();
                    }
                },3000
        );
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if(requestCode == REQUEST_SIGNUP){
            if(resultCode == RESULT_OK){
                this.finish();
            }
        }
    }


    @Override
    public void onBackPressed() {
        moveTaskToBack(true);
    }

    public void onLoginSucess(){
        _loginButton.setEnabled(true);
        finish();
    }

    public void onLoginFailed(){
        Toast.makeText(MainActivity.this, "Fallo el inicio de sesión", Toast.LENGTH_LONG).show();
        _loginButton.setEnabled(true);
    }

    public boolean validate(){
        boolean valid = true;
        String email = _emailText.getText().toString();
        String password = _passwordText.getText().toString();

        if(email.isEmpty() || !Patterns.EMAIL_ADDRESS.matcher(email).matches()){
            _emailText.setError("Ingrese un cuenta de correo valida");
        }else{
            _emailText.setError(null);
        }

        if (password.isEmpty() || password.length() < 8 || password.length() > 20) {
            _passwordText.setError("El password debe tener por lo menos 8 caracteres de longitud");
            valid = false;
        } else {
            _passwordText.setError(null);
        }
        return valid;
    }

    /*@Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }*/

    /*@Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }*/
}
