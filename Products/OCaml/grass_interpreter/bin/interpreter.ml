open GrassLang

exception Eval_Error of string

type value =
| VFun of (env * int * app list)
| VChar of int
| VIn
| VOut
| VSucc
and env = value list

let fstenv = [VOut; VSucc; VChar 119; VIn]

let funid = VFun([],1,[])
let funtru = VFun ([funid], 2, App (3,2)::[])
let funfls = VFun ([],2,[])

let rec eval = function
| Prog (hd, cl) 
  -> let env = fstenv in
    let env = eval_abs env hd in
    let env =
      List.fold_left
      (fun en comm -> eval_comm en comm)
      env cl
    in
    eval_app env (App (1,1))
and eval_comm (env:env) (comm:comm) :env = match comm with
| CAbs ab -> eval_abs env ab
| CApp ap -> eval_app env ap
and eval_abs (env:env) (Abs (xc, body)) :env =
  (VFun (env, xc,body))::env
and eval_app (env:env) (App (fc, vc)) :env = 
  let envsize = List.length env in
  if fc < 1 && vc < 1 && envsize < fc && envsize < vc 
  then raise (Eval_Error "index is out of length of stack")
  else 
    match List.nth env (fc - 1) , List.nth env (vc - 1) with
    | VFun (_, n, _), _ when n < 0 
      -> raise (Eval_Error "Unexpected error: function value's argument is nagative")
    | VFun (fenv, 1, body), v
      -> (match List.fold_left eval_app (v::fenv) body with
        | res::_ -> res ::env
        | [] -> raise (Eval_Error "Unexpected error: Stack is empty"))
    | VFun (fenv, n, body), v -> VFun (v::fenv,(n-1),body)::env
    | VChar c1, VChar c2 when c1 = c2 -> funtru::env
    | VChar _, _ -> funfls::env
    | VIn, v 
      -> (match In_channel.input_byte stdin with
        | Some n -> VChar n :: env
        | None -> v::env)
    | VOut, (VChar n as v) -> output_char stdout (char_of_int n); v::env
    | VOut, _ -> raise (Eval_Error "Eval_error:function Out can accept only character.")
    | VSucc, VChar n -> VChar (if n = 255 then 0 else n+1)::env
    | VSucc, _ -> raise (Eval_Error "Eval_error:function Succ can accept only character.")
