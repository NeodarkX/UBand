package pe.com.uband.uband.Tabs;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.util.ArrayList;
import java.util.List;

import Models.User;
import pe.com.uband.uband.Adapters.RecyclerViewAdapter;
import pe.com.uband.uband.Fragments.BaseVolleyFragment;
import pe.com.uband.uband.R;

public class MembersTabFragment extends BaseVolleyFragment {
    RecyclerViewAdapter adapter;

    public MembersTabFragment() {
        // Required empty public constructor
    }

    // TODO: Rename and change types and number of parameters
    public static MembersTabFragment newInstance(String param1, String param2) {
        MembersTabFragment fragment = new MembersTabFragment();
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
        View rootView = inflater.inflate(R.layout.fragment_members_tab, container, false);
        User user =new User();
        user.setNombres("harold");
        List<User> luser = new ArrayList<User>();
        luser.add(user);

        RecyclerView rv = (RecyclerView)rootView.findViewById(R.id.myList);
        rv.setHasFixedSize(true);

        LinearLayoutManager llm = new LinearLayoutManager(getActivity());
        rv.setLayoutManager(llm);

        adapter = new RecyclerViewAdapter(luser);
        rv.setAdapter(adapter);
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
