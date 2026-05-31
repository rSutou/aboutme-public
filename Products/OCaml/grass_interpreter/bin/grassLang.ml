type prog =
| Prog of (abs * comm list)
and comm =
| CAbs of abs
| CApp of app
and abs =
| Abs of (int * app list)
and app =
| App of (int * int)


let rec string_of_prog = function
| Prog (ab, cl) 
  -> List.fold_left
    (fun e c -> e ^ "v" ^ string_of_comm c)
    (string_of_abs ab)
    cl
and string_of_comm = function
| CAbs ab -> string_of_abs ab
| CApp ap -> string_of_app ap
and string_of_abs = function
| Abs (xc, body) 
  -> List.fold_left
    (fun e ap -> e ^ string_of_app ap)
    (String.init xc (fun _ -> 'w' )) body
and string_of_app = function
| App (fc,vc) -> String.init fc (fun _ -> 'W' ) ^ String.init vc (fun _ -> 'w' )