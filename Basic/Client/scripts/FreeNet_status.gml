///FreeNet_status()
var timeout = current_time - ping[0];
if(timeout > 5000) {
    FreeNet_disconnect();
}
if(FreeNet_isConnected() == -1) {
    FreeNet_reconnect();
    global.login = false;
    room_goto(rm_connect);
}
if(FreeNet_isConnected() == 1) {
    if(room != rm_login)and(global.login == false) {
        room_goto(rm_login);
    }
}
