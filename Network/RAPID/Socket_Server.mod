MODULE Socket_Server
    ! The socket communication and other variables.
    VAR socketdev server_socket;
    VAR socketdev client_socket;
    VAR string recv_string1;
    VAR string recv_method;
    VAR string recv_speed;
    VAR string recv_ext;
    VAR string recv_pos_robjoint;
    VAR string recv_orient;
    VAR string recv_next_target;
    VAR pos to_point_pos;
    VAR orient to_point_orient;
    VAR extjoint ext_joint;
    VAR robjoint to_rob_joint;
    VAR bool keep_receiving_targets;
    VAR bool filler_bool;

    VAR confdata conf:=[0,0,0,0];
    PERS tooldata T_TEXT_01:=[TRUE,[[-90.442,-25.934,132.491],[0.77729,-0.25919,-0.06870,0.56914]],[3.000,[-90.442,-25.934,132.491],[1,0,0,0],0,0,0]];
    TASK PERS wobjdata DefaultFrame:=[FALSE,TRUE,"",[[0.000,0.000,0.000],[1.00000,0.00000,0.00000,0.00000]],[[0,0,0],[1,0,0,0]]];
    TASK PERS speeddata Speed000:=[200.000,180.000,5000.000,1080.000];
    TASK PERS speeddata Speed001:=[20.000,180.000,5000.000,1080.000];
    TASK PERS zonedata Zone000:=[FALSE,1.000,1.000,1.000,0.100,1.000,0.100];
    TASK PERS zonedata Zone001:=[FALSE,1.000,1.000,1.000,0.100,1.000,0.100];

    VAR speeddata current_speed;

    PROC main()
        ConfL\Off;
        SocketCreate server_socket;
        SocketBind server_socket,"10.0.0.13",1025;
        SocketListen server_socket;

        WHILE TRUE DO
            SocketAccept server_socket,client_socket\Time:=WAIT_MAX;
            keep_receiving_targets:=TRUE;
            recv_string1:=ClientSync();
            IF (recv_string1="ok") THEN
                WHILE keep_receiving_targets DO
                    SocketReceive client_socket\Str:=recv_method\Time:=3;
                    SocketReceive client_socket\Str:=recv_speed\Time:=3;
                    SocketReceive client_socket\Str:=recv_ext\Time:=3;
                    SocketReceive client_socket\Str:=recv_pos_robjoint\Time:=3;


                    IF recv_speed="Speed000" THEN
                        current_speed:=Speed000;
                    ELSEIF recv_speed="Speed001" THEN
                        current_speed:=Speed001;
                    ENDIF

                    filler_bool:=StrToVal(recv_ext,ext_joint);

                    IF recv_method="MoveL" THEN
                        SocketReceive client_socket\Str:=recv_orient\Time:=3;
                        filler_bool:=StrToVal(recv_pos_robjoint,to_point_pos);
                        filler_bool:=StrToVal(recv_orient,to_point_orient);
                        !MoveL [to_point_pos,to_point_orient,conf,ext_joint],current_speed,Zone001,T_TEXT_01 \WObj:=DefaultFrame;    
                    ELSEIF recv_method="MoveAbsJ" THEN
                        filler_bool:=StrToVal(recv_pos_robjoint,to_rob_joint);
                        MoveAbsJ [to_rob_joint,ext_joint],current_speed,Zone000,T_TEXT_01;
                    ENDIF

                    WaitTime\InPos,0;
                    SocketSend client_socket\Str:="ready";
                    SocketReceive client_socket\Str:=recv_next_target\Time:=3;
                    IF recv_next_target="No more targets" THEN
                        keep_receiving_targets:=FALSE;
                        SocketClose client_socket;
                    ENDIF
                ENDWHILE
            ENDIF
        ENDWHILE
        !ERROR
        ! IF ERRNO=ERR_SOCK_TIMEOUT THEN
        ! keep_scanning := FALSE;
        !RETURN;
        !ENDIF
    UNDO
        SocketClose client_socket;
        SocketClose server_socket;
    ENDPROC

    ! Wait until the client sends a string.
    FUNC string ClientSync()
        VAR string recv_string;
        SocketReceive client_socket\Str:=recv_string\Time:=WAIT_MAX;
        IF recv_string="listening?" THEN
            SocketSend client_socket\Str:="Yes";
            RETURN "ok";
        ELSEIF recv_string="stop" THEN
            SocketSend client_socket\Str:="ok";
            RETURN "stop";
        ELSE
            !switching protocols

            RETURN "unexpect str:";
        ENDIF
    ENDFUNC

    FUNC string GetTargets()
        VAR string recv_string;
        SocketReceive client_socket\Str:=recv_string\Time:=WAIT_MAX;
    ENDFUNC


ENDMODULE
