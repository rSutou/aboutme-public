%{
    open GrassLang
%}


%token SW
%token LW
%token SV
%token EOF


%start main
%start commands


%type <GrassLang.prog> main
%type <GrassLang.comm list> commands

%%



main:
    | abs commands { Prog ($1,$2) }
;

commands:
	| SV abs commands 			{ CAbs $2 :: $3 }
	| SV appcommands commands 	{ $2 @ $3 }
	| EOF						{ [] }
;
appcommands:
	| app appcommands		{ CApp $1 :: $2 }
	| app					{ CApp $1 :: [] }
;

abs:
	| sws apps 	{ Abs ($1,$2) }
;

apps:
	| app apps 	{ $1::$2 }
	|  			{ [] }
;

app:
	| lws sws 	{ App ($1,$2) }
;

sws:
	| SW sws	{ 1 + $2 }
	| SW		{ 1 }
;

lws:
	| LW lws	{ 1 + $2 }
	| LW		{ 1 }
;