# What's this
一般に何回プログラミング言語と言われるジャンルに属する、Grass という言語のインタプリタです。
Grass言語の参考 https://scrapbox.io/kembo/Grass




# 実行方法
ocamlc および ocamllex, menhir, dune パッケージが必要です。

dune build 
でビルドできます。ビルドされたファイルの中の /_build/default/bin/main.exe が実行ファイルです。
実行時引数にファイル名を渡せばテキストファイルを読み取って実行できます。

dune exec grass_interpreter
で実行できます。

# 改善目標
- Grass 言語の構文はとてもシンプルなので ocamllex や menhir などのパッケージに頼る必要はない。
- 難解言語なので有用性が伝わりにくい
