package pe.com.uband.uband.Activities;

import android.net.Uri;
import android.os.Bundle;
import android.os.PersistableBundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.squareup.picasso.Picasso;

import java.util.HashMap;

import Models.SessionManager;
import pe.com.uband.uband.Fragments.HomeFragment;
import pe.com.uband.uband.R;

public class PrincipalActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener{

    private NavigationView navDrawer;
    private DrawerLayout drawerLayout;
    private ActionBarDrawerToggle drawerToggle;
    private int selectedItem;
    private SessionManager session;
    Fragment fragment;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_principal);

        session = new SessionManager(getApplicationContext());
        if(session.checkLogin())
            finish();

        Toolbar toolbar = (Toolbar) findViewById(R.id.my_awesome_toolbar);
        setSupportActionBar(toolbar);
        drawerLayout = (DrawerLayout) findViewById(R.id.drawer_layout);
        navDrawer = (NavigationView) findViewById(R.id.menu_drawer);
        navDrawer.setNavigationItemSelectedListener(this);
        drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, R.string.drawer_open, R.string.drawer_close);
        drawerLayout.addDrawerListener(drawerToggle);
        drawerToggle.syncState();

        View hView =  navDrawer.getHeaderView(0);
        HashMap<String, String> user = session.getUserDetails();
        TextView nav_user = (TextView)hView.findViewById(R.id.textViewName);
        nav_user.setText(user.get(SessionManager.KEY_NAME));
        ImageView iv = (ImageView) hView.findViewById(R.id.imageView);
        String image = user.get(SessionManager.KEY_IMAGE);
        image = image.substring(1,image.length());

        Picasso.with(PrincipalActivity.this).load("http://faceband.azurewebsites.net"+image).into(iv);
        selectedItem = savedInstanceState == null ? R.id.nav_item_1 : savedInstanceState.getInt("selectedItem");

        FirstFragmentView(true);

    }

    @Override
    public boolean onNavigationItemSelected(MenuItem menuItem) {
        menuItem.setChecked(true);
        selectedItem = menuItem.getItemId();

        switch (selectedItem) {
            case R.id.nav_item_1:
                FirstFragmentView(false);
                break;
            case R.id.nav_item_4:
                session.logoutUser();
                break;
        }

        drawerLayout.closeDrawer(GravityCompat.START);

        return true;
    }

    public void FirstFragmentView(boolean first){
        FragmentManager fragmentManager = getSupportFragmentManager();
        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
        HomeFragment hello = new HomeFragment();
        hello.newInstance(SessionManager.KEY_NAME,SessionManager.KEY_EMAIL);
        if(first)
            fragmentTransaction.add(R.id.fragment, hello);
        else

            fragmentTransaction.replace(R.id.fragment, hello);
        fragmentTransaction.commit();
    }


    @Override
    public void onBackPressed() {

        if (drawerLayout.isDrawerOpen(GravityCompat.START)) {
            drawerLayout.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public void onSaveInstanceState(Bundle outState, PersistableBundle outPersistentState) {
        super.onSaveInstanceState(outState, outPersistentState);

        outState.putInt("selectedItem", selectedItem);
    }

    public void onFragmentInteraction(Uri uri){
        Toast toast = Toast.makeText(this, "Wheeee!",Toast.LENGTH_SHORT);
        toast.show();
    }

}
