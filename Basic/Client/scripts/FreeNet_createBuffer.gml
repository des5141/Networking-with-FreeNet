///FreeNet_createBuffer();
var buffer = buffer_create(1024, buffer_grow, 1);
buffer_write(buffer, buffer_u32, 0);
return buffer;
