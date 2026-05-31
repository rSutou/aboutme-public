open Interpreter

let eval_from_string (prog:string) :unit = 
  let lexbuf = Lexing.from_string prog in 
  try 
    let result = Parser.main Lexer.token lexbuf in
    let _ = eval result in ()
  with 
    | Failure s -> print_endline s
    | Parsing.Parse_error (* ocamlyacc *) | Parser.Error (* menhir *) -> 
      Printf.printf "Parse Error"; print_newline ()
    | Eval_Error s -> print_endline ("eval_error: " ^ s)

let rec read_eval_print () :unit = 
  let instr = read_line () in
    eval_from_string instr; read_eval_print ()
    
let read_from_file (inch:in_channel) (filename:string) : unit = 
  let lexbuf = Lexing.from_channel inch in 
  let _ = Lexing.set_filename lexbuf filename in  
  try 
    let result = Parser.main Lexer.token lexbuf in
    let _ = eval result in ()
  with 
    | Failure s -> print_endline s
    | Parsing.Parse_error (* ocamlyacc *) | Parser.Error (* menhir *) -> 
      Printf.printf "Parse Error" 
    | Eval_Error s -> print_endline ("eval_error: " ^ s)

let filenames:string list ref = ref []
let spec = []

let _ = 
  Arg.parse spec 
  (fun s -> filenames:= s::!filenames)
  "Usage: main [filename]";
  match !filenames with
  | [] -> read_eval_print ()
  | n::_ -> let inch = open_in n in 
      (try read_from_file inch n with 
      | Eval_Error s -> print_endline s 
      |_-> "Error in reading and evaluating from the file" |> print_endline);
      close_in inch 




