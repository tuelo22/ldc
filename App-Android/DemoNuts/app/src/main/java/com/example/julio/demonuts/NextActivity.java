package com.example.julio.demonuts;

import android.support.v7.app.AppCompatActivity;
import android.widget.TextView;
import android.os.Bundle;


public class NextActivity extends AppCompatActivity {

    private TextView tv;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_next);

        tv = (TextView) findViewById(R.id.tv);

        for (int i = 0; i < 5; i++) {
            String text = tv.getText().toString();
            tv.setText(text + MainActivity.modelArrayList.get(i).getFruit()+" -> "+MainActivity.modelArrayList.get(i).getNumber()+"\n");
        }
    }
}