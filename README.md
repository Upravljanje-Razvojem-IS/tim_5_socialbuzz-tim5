# README

1. git clone https://github.com/Upravljanje-Razvojem-IS/tim_5_socialbuzz-tim5.git // ovo radite samo jednom\
2. git checkout -b "ime-funkcionalnost" (sebastijan-autentifikacija npr.) // pravite svoju granu\
// obratite paznju da komitujete uvek na svoju granu, a ne na main granu\
3. git add .\
4. git status // proverite da li ste stage-ovali sve fajlove\
5. git commit -m "opis koju funkcionalnost ste dodali"\

--- DODATNE KOMANDE\
git branch -a // izlistava sve grane ukljucujuci i main\
git checkout "ime grane" // prelaz sa jedne grane na drugu\
git log --oneline // izlistava sve komitove\

// Vracanje nazad u vreme\
git checkout "id komita" (read-only)\
git revert "id komita" (moguca modifikacija komita)\
git reset "id komita" (brise sve nakon ovog komita)
