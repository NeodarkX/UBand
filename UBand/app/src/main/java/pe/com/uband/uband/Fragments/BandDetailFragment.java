package pe.com.uband.uband.Fragments;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTabHost;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import pe.com.uband.uband.R;
import pe.com.uband.uband.Tabs.PerfilTabFragment;

public class BandDetailFragment extends Fragment {
    private FragmentTabHost tabHost;

    public BandDetailFragment() {
        // Required empty public constructor
    }


    public static BandDetailFragment newInstance(String param1, String param2) {
        BandDetailFragment fragment = new BandDetailFragment();
        Bundle args = new Bundle();
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View rootView = inflater.inflate(R.layout.fragment_band_detail, container, false);

        tabHost = (FragmentTabHost) rootView.findViewById(R.id.tabhost);
        tabHost.setup(getActivity(),getChildFragmentManager(),R.id.tabcontent);

        tabHost.addTab(tabHost.newTabSpec("Tab1").setIndicator("Perfil"), PerfilTabFragment.class,null);

        return rootView;
    }

    // TODO: Rename method, update argument and hook method into UI event
    public void onButtonPressed(Uri uri) {
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
    }

    @Override
    public void onDetach() {
        super.onDetach();
    }
}
