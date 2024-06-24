˚
ZC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Startup.cs
	namespace 	
CommentService
 
{ 
public 

class 
Startup 
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
void 
ConfigureServices %
(% &
IServiceCollection& 8
services9 A
)A B
{ 	
services 
. 
AddDbContext !
<! "
CommentContext" 0
>0 1
(1 2
options 
=> 
options "
." #
UseSqlServer# /
(/ 0
Configuration0 =
.= >
GetConnectionString> Q
(Q R
$strR [
)[ \
)\ ]
)] ^
;^ _
services 
. 
AddControllers #
(# $
)$ %
;% &
services   
.   
	AddScoped   
<   
ICommentRepository   1
,  1 2
CommentRepository  3 D
>  D E
(  E F
)  F G
;  G H
services!! 
.!! 
	AddScoped!! 
<!! 
IAccountRepository!! 1
,!!1 2!
MockAccountRepository!!3 H
>!!H I
(!!I J
)!!J K
;!!K L
services## 
.## 
	AddScoped## 
<## 
LoggerL## &
>##& '
(##' (
)##( )
;##) *
services$$ 
.$$ 
AddAutoMapper$$ "
($$" #
	AppDomain$$# ,
.$$, -
CurrentDomain$$- :
.$$: ;
GetAssemblies$$; H
($$H I
)$$I J
)$$J K
;$$K L
services%% 
.%% 
AddSwaggerGen%% "
(%%" #
)%%# $
;%%$ %
}&& 	
public)) 
void)) 
	Configure)) 
()) 
IApplicationBuilder)) 1
app))2 5
,))5 6
IWebHostEnvironment))7 J
env))K N
)))N O
{** 	
if++ 
(++ 
env++ 
.++ 
IsDevelopment++ !
(++! "
)++" #
)++# $
{,, 
app-- 
.-- %
UseDeveloperExceptionPage-- -
(--- .
)--. /
;--/ 0
}.. 
app00 
.00 
UseHttpsRedirection00 #
(00# $
)00$ %
;00% &
app22 
.22 

UseRouting22 
(22 
)22 
;22 
app44 
.44 
UseAuthorization44  
(44  !
)44! "
;44" #
app66 
.66 
UseEndpoints66 
(66 
	endpoints66 &
=>66' )
{77 
	endpoints88 
.88 
MapControllers88 (
(88( )
)88) *
;88* +
}99 
)99 
;99 
app;; 
.;; 

UseSwagger;; 
(;; 
);; 
;;; 
app== 
.== 
UseSwaggerUI== 
(== 
e== 
=>== !
{>> 
e?? 
.?? 
SwaggerEndpoint?? !
(??! "
$str??" <
,??< =
$str??> S
)??S T
;??T U
e@@ 
.@@ 
RoutePrefix@@ 
=@@ 
string@@  &
.@@& '
Empty@@' ,
;@@, -
}AA 
)AA 
;AA 
}BB 	
}CC 
}DD ∂

ZC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Program.cs
	namespace

 	
CommentService


 
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	
CreateHostBuilder 
( 
args "
)" #
.# $
Build$ )
() *
)* +
.+ ,
Run, /
(/ 0
)0 1
;1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
. $
ConfigureWebHostDefaults )
() *

webBuilder* 4
=>5 7
{ 

webBuilder 
. 

UseStartup )
<) *
Startup* 1
>1 2
(2 3
)3 4
;4 5
} 
) 
; 
} 
} ¥
iC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Profiles\CommentMapper.cs
	namespace 	
CommentService
 
. 
Profiles !
{ 
public 

class 
CommentMapper 
:  
Profile! (
{ 
public		 
CommentMapper		 
(		 
)		 
{

 	
	CreateMap 
< 
Comment 
, 
CommentReadDto -
>- .
(. /
)/ 0
;0 1
	CreateMap 
< 
CommentCreateDto &
,& '
Comment( /
>/ 0
(0 1
)1 2
;2 3
	CreateMap 
< 
CommentUpdateDto &
,& '
Comment( /
>/ 0
(0 1
)1 2
;2 3
} 	
} 
} ®
aC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Models\Comment.cs
	namespace 	
CommentService
 
. 
Models 
{ 
public 

class 
Comment 
{ 
public

 
int

 
Id

 
{

 
get

 
;

 
set

  
;

  !
}

" #
public 
Guid 

AccountUid 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
AdId 
{ 
get 
; 
set "
;" #
}$ %
public 
string 
Text 
{ 
get  
;  !
set" %
;% &
}' (
public 
DateTime 
TimeAndDate #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
}   È
\C:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Models\Ad.cs
	namespace 	
CommentService
 
. 
Models 
{ 
public 

class 
Ad 
{ 
public

 
int

 
AdId

 
{

 
get

 
;

 
set

  #
;

# $
}

% &
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Content 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} Ä
aC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Models\Account.cs
	namespace 	
CommentService
 
. 
Models 
{ 
public 

class 
Account 
{ 
public

 
Guid

 

AccountUid

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
string 
	FirstName 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
LastName 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} Ü
tC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Migrations\20210921151914_Initial.cs
	namespace 	
CommentService
 
. 

Migrations #
{ 
public 

partial 
class 
Initial  
:! "
	Migration# ,
{ 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{		 	
migrationBuilder

 
.

 
CreateTable

 (
(

( )
name 
: 
$str  
,  !
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
int& )
>) *
(* +
type+ /
:/ 0
$str1 6
,6 7
nullable8 @
:@ A
falseB G
)G H
. 

Annotation #
(# $
$str$ 8
,8 9
$str: @
)@ A
,A B

AccountUid 
=  
table! &
.& '
Column' -
<- .
Guid. 2
>2 3
(3 4
type4 8
:8 9
$str: L
,L M
nullableN V
:V W
falseX ]
)] ^
,^ _
AdId 
= 
table  
.  !
Column! '
<' (
int( +
>+ ,
(, -
type- 1
:1 2
$str3 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K
Text 
= 
table  
.  !
Column! '
<' (
string( .
>. /
(/ 0
type0 4
:4 5
$str6 D
,D E
nullableF N
:N O
trueP T
)T U
,U V
TimeAndDate 
=  !
table" '
.' (
Column( .
<. /
DateTime/ 7
>7 8
(8 9
type9 =
:= >
$str? J
,J K
nullableL T
:T U
falseV [
)[ \
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 2
,2 3
x4 5
=>6 8
x9 :
.: ;
Id; =
)= >
;> ?
} 
) 
; 
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
	DropTable &
(& '
name 
: 
$str  
)  !
;! "
} 	
}   
}!! Ë
aC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Logger\LoggerL.cs
	namespace 	
CommentService
 
. 
Logger 
{ 
public 

class 
LoggerL 
{ 
private 
readonly 
ILogger  
<  !
LoggerL! (
>( )
_logger* 1
;1 2
public		 
LoggerL		 
(		 
ILogger		 
<		 
LoggerL		 &
>		& '
logger		( .
)		. /
{

 	
_logger 
= 
logger 
; 
} 	
public 
void 
Log 
( 
string 
message &
)& '
{ 	
_logger 
. 
LogInformation "
(" #
message# *
)* +
;+ ,
} 	
} 
} ‰
hC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\DTOs\CommentUpdateDto.cs
	namespace 	
CommentService
 
. 
DTOs 
{ 
public 

class 
CommentUpdateDto !
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Text 
{ 
get  
;  !
set" %
;% &
}' (
} 
} ≤
fC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\DTOs\CommentReadDto.cs
	namespace 	
CommentService
 
. 
DTOs 
{ 
public 

class 
CommentReadDto 
{ 
public

 
int

 
Id

 
{

 
get

 
;

 
set

  
;

  !
}

" #
public 
Guid 

AccountUid 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
AdId 
{ 
get 
; 
set "
;" #
}$ %
public 
string 
Text 
{ 
get  
;  !
set" %
;% &
}' (
public 
DateTime 
TimeAndDate #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
}   Ç
hC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\DTOs\CommentCreateDto.cs
	namespace 	
CommentService
 
. 
DTOs 
{ 
public 

class 
CommentCreateDto !
{ 
public

 
Guid

 

AccountUid

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
int 
AdId 
{ 
get 
; 
set "
;" #
}$ %
public 
string 
Text 
{ 
get  
;  !
set" %
;% &
}' (
} 
} æ
vC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Data\Repositories\CommentRepository.cs
	namespace 	
CommentService
 
. 
Data 
. 
Repositories *
{		 
public

 

class

 
CommentRepository

 "
:

# $
ICommentRepository

% 7
{ 
private 
readonly 
CommentContext '
_context( 0
;0 1
public 
CommentRepository  
(  !
CommentContext! /
context0 7
)7 8
{ 	
_context 
= 
context 
; 
} 	
public 
IEnumerable 
< 
Comment "
>" #
Get$ '
(' (
)( )
{ 	
return 
_context 
. 
Comments $
;$ %
} 	
public 
Comment 
Get 
( 
int 
id !
)! "
{ 	
return 
_context 
. 
Comments $
. 
Where 
( 
comment 
=> !
comment" )
.) *
Id* ,
==- /
id0 2
)2 3
. 
FirstOrDefault 
(  
)  !
;! "
} 	
public 
void 
Create 
( 
Comment "
comment# *
)* +
{   	
_context!! 
.!! 
Comments!! 
.!! 
Add!! !
(!!! "
comment!!" )
)!!) *
;!!* +
_context"" 
."" 
SaveChanges""  
(""  !
)""! "
;""" #
}## 	
public%% 
void%% 
Update%% 
(%% 
Comment%% "
comment%%# *
)%%* +
{&& 	
_context'' 
.'' 
Entry'' 
('' 
comment'' "
)''" #
.''# $
State''$ )
=''* +
EntityState'', 7
.''7 8
Modified''8 @
;''@ A
_context(( 
.(( 
SaveChanges((  
(((  !
)((! "
;((" #
}** 	
public,, 
void,, 
Delete,, 
(,, 
Comment,, "
comment,,# *
),,* +
{-- 	
_context.. 
... 
Comments.. 
... 
Remove.. $
(..$ %
comment..% ,
).., -
;..- .
_context// 
.// 
SaveChanges//  
(//  !
)//! "
;//" #
}00 	
}11 
}22 Ú
nC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Data\Mocks\MockAdRepository.cs
	namespace 	
CommentService
 
. 
Data 
. 
Repositories *
{ 
public		 

class		 
MockAdRepository		 !
:		" #
IAdRepository		$ 1
{

 
private 
static 
List 
< 
Ad 
> 
posts  %
=& '
new( +
List, 0
<0 1
Ad1 3
>3 4
{ 	
new 
Ad 
{ 
AdId 
= 
$num 
, 
Name 
= 
$str  
,  !
Content 
= 
$str	 ó
} 
, 
new 
Ad 
{ 
AdId 
= 
$num 
, 
Name 
= 
$str %
,% &
Content 
= 
$str	 Ù
} 
, 
} 	
;	 

public 
Ad 
Get 
( 
int 
adId 
) 
{ 	
return 
posts 
. 
Where 
( 
a  
=>! #
a$ %
.% &
AdId& *
==+ -
adId. 2
)2 3
.3 4
SingleOrDefault4 C
(C D
)D E
;E F
} 	
}   
}!! À
sC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Data\Mocks\MockAccountRepository.cs
	namespace 	
CommentService
 
. 
Data 
. 
Repositories *
{ 
public		 

class		 !
MockAccountRepository		 &
:		' (
IAccountRepository		) ;
{

 
private 
static 
List 
< 
Account #
># $
	_accounts% .
=/ 0
new1 4
List5 9
<9 :
Account: A
>A B
{ 	
new 
Account 
{ 

AccountUid 
= 
Guid !
.! "
Parse" '
(' (
$str( N
)N O
,O P
	FirstName 
= 
$str "
," #
LastName 
= 
$str #
} 
, 
new 
Account 
{ 

AccountUid 
= 
Guid !
.! "
Parse" '
(' (
$str( N
)N O
,O P
	FirstName 
= 
$str #
,# $
LastName 
= 
$str  
} 
, 
new 
Account 
{ 

AccountUid 
= 
Guid !
.! "
Parse" '
(' (
$str( N
)N O
,O P
	FirstName 
= 
$str "
," #
LastName 
= 
$str !
}   
}!! 	
;!!	 

public## 
Account## 
Get## 
(## 
Guid## 

accoutnUid##  *
)##* +
{$$ 	
return%% 
	_accounts%% 
.%% 
Where%% "
(%%" #
a%%# $
=>%%% '
a%%( )
.%%) *

AccountUid%%* 4
==%%5 7

accoutnUid%%8 B
)%%B C
.%%C D
SingleOrDefault%%D S
(%%S T
)%%T U
;%%U V
}&& 	
}'' 
}(( Ë
uC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Data\Interfaces\ICommentRepository.cs
	namespace 	
CommentService
 
. 
Data 
. 

Interfaces (
{ 
public 

	interface 
ICommentRepository '
{ 
IEnumerable 
< 
Comment 
> 
Get  
(  !
)! "
;" #
Comment		 
Get		 
(		 
int		 
id		 
)		 
;		 
void

 
Create

 
(

 
Comment

 
comment

 #
)

# $
;

$ %
void 
Update 
( 
Comment 
comment #
)# $
;$ %
void 
Delete 
( 
Comment 
comment #
)# $
;$ %
} 
} ⁄
pC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Data\Interfaces\IAdRepository.cs
	namespace 	
CommentService
 
. 
Data 
. 

Interfaces (
{ 
public 

	interface 
IAdRepository "
{ 
Ad 

Get 
( 
int 
adId 
) 
; 
}		 
}

 ÷
fC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Data\CommentContext.cs
	namespace 	
CommentService
 
. 
Data 
{ 
public 

class 
CommentContext 
:  !
	DbContext" +
{ 
public 
DbSet 
< 
Comment 
> 
Comments &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public

 
CommentContext

 
(

 
DbContextOptions

 .
<

. /
CommentContext

/ =
>

= >
options

? F
)

F G
:

H I
base

J N
(

N O
options

O V
)

V W
{

X Y
}

Z [
} 
} 
uC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Data\Interfaces\IAccountRepository.cs
	namespace 	
CommentService
 
. 
Data 
. 

Interfaces (
{ 
public 

	interface 
IAccountRepository '
{ 
Account 
Get 
( 
Guid 

accoutnUid #
)# $
;$ %
}		 
}

 Ûg
pC:\Users\zoran\Desktop\uris\tim_5_socialbuzz-tim5\CommentService\CommentService\Controllers\CommentController.cs
	namespace 	
CommentService
 
. 
Controllers $
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
CommentsController #
:$ %
ControllerBase& 4
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
ICommentRepository +
_commentRepository, >
;> ?
private 
readonly 
IAccountRepository +
_accountRepository, >
;> ?
private 
readonly 
LoggerL  
_logger! (
;( )
public 
CommentsController !
(! "
IMapper" )
mapper* 0
,0 1
ICommentRepository2 D
commentRepositoryE V
,V W
LoggerLX _
logger` f
,f g
IAccountRepositoryh z
accountRepository	{ å
)
å ç
{ 	
_mapper 
= 
mapper 
; 
_commentRepository 
=  
commentRepository! 2
;2 3
_logger 
= 
logger 
; 
_accountRepository 
=  
accountRepository! 2
;2 3
} 	
[(( 	 
ProducesResponseType((	 
((( 
StatusCodes(( )
.(() *
Status200OK((* 5
)((5 6
]((6 7
[)) 	 
ProducesResponseType))	 
()) 
StatusCodes)) )
.))) *
Status400BadRequest))* =
)))= >
]))> ?
[** 	 
ProducesResponseType**	 
(** 
StatusCodes** )
.**) *!
Status401Unauthorized*** ?
)**? @
]**@ A
[++ 	 
ProducesResponseType++	 
(++ 
StatusCodes++ )
.++) *(
Status500InternalServerError++* F
)++F G
]++G H
[,, 	
HttpPost,,	 
],, 
public-- 
ActionResult-- 
<-- 
CommentReadDto-- *
>--* +
Create--, 2
(--2 3
[--3 4
FromBody--4 <
]--< =
CommentCreateDto--> N
commentCreateDto--O _
)--_ `
{.. 	
var// 
account// 
=// 
_accountRepository// ,
.//, -
Get//- 0
(//0 1
commentCreateDto//1 A
.//A B

AccountUid//B L
)//L M
;//M N
if00 
(00 
account00 
==00 
null00 
)00  
{11 
return22 

BadRequest22 !
(22! "
$str22" 5
)225 6
;226 7
}33 
Comment55 
comment55 
=55 
_mapper55 %
.55% &
Map55& )
<55) *
Comment55* 1
>551 2
(552 3
commentCreateDto553 C
)55C D
;55D E
comment66 
.66 
TimeAndDate66 
=66  !
DateTime66" *
.66* +
UtcNow66+ 1
;661 2
_commentRepository77 
.77 
Create77 %
(77% &
comment77& -
)77- .
;77. /
_logger88 
.88 
Log88 
(88 
$str88 )
)88) *
;88* +
return99 
Ok99 
(99 
_mapper99 
.99 
Map99 !
<99! "
CommentReadDto99" 0
>990 1
(991 2
comment992 9
)999 :
)99: ;
;99; <
}:: 	
[CC 	 
ProducesResponseTypeCC	 
(CC 
StatusCodesCC )
.CC) *
Status200OKCC* 5
)CC5 6
]CC6 7
[DD 	 
ProducesResponseTypeDD	 
(DD 
StatusCodesDD )
.DD) *!
Status401UnauthorizedDD* ?
)DD? @
]DD@ A
[EE 	 
ProducesResponseTypeEE	 
(EE 
StatusCodesEE )
.EE) *(
Status500InternalServerErrorEE* F
)EEF G
]EEG H
[FF 	
HttpGetFF	 
]FF 
publicGG 
ActionResultGG 
<GG 
IEnumerableGG '
<GG' (
CommentReadDtoGG( 6
>GG6 7
>GG7 8
GetGG9 <
(GG< =
)GG= >
{HH 	
ListII 
<II 
CommentII 
>II 
resultII  
=II! "
_commentRepositoryII# 5
.II5 6
GetII6 9
(II9 :
)II: ;
.II; <
ToListII< B
(IIB C
)IIC D
;IID E
_loggerJJ 
.JJ 
LogJJ 
(JJ 
$strJJ &
)JJ& '
;JJ' (
returnKK 
OkKK 
(KK 
_mapperKK 
.KK 
MapKK !
<KK! "
IEnumerableKK" -
<KK- .
CommentReadDtoKK. <
>KK< =
>KK= >
(KK> ?
resultKK? E
)KKE F
)KKF G
;KKG H
}LL 	
[VV 	 
ProducesResponseTypeVV	 
(VV 
StatusCodesVV )
.VV) *
Status200OKVV* 5
)VV5 6
]VV6 7
[WW 	 
ProducesResponseTypeWW	 
(WW 
StatusCodesWW )
.WW) *
Status400BadRequestWW* =
)WW= >
]WW> ?
[XX 	 
ProducesResponseTypeXX	 
(XX 
StatusCodesXX )
.XX) *!
Status401UnauthorizedXX* ?
)XX? @
]XX@ A
[YY 	 
ProducesResponseTypeYY	 
(YY 
StatusCodesYY )
.YY) *(
Status500InternalServerErrorYY* F
)YYF G
]YYG H
[ZZ 	
HttpGetZZ	 
(ZZ 
$strZZ 
)ZZ 
]ZZ 
public[[ 
ActionResult[[ 
<[[ 
CommentReadDto[[ *
>[[* +
Get[[, /
([[/ 0
int[[0 3
id[[4 6
)[[6 7
{\\ 	
if]] 
(]] 
_commentRepository]] "
.]]" #
Get]]# &
(]]& '
id]]' )
)]]) *
==]]+ -
null]]. 2
)]]2 3
{^^ 
return__ 

BadRequest__ !
(__! "
$str__" G
)__G H
;__H I
}`` 
Commentbb 
resultbb 
=bb 
_commentRepositorybb /
.bb/ 0
Getbb0 3
(bb3 4
idbb4 6
)bb6 7
;bb7 8
_loggercc 
.cc 
Logcc 
(cc 
$strcc %
)cc% &
;cc& '
returndd 
Okdd 
(dd 
_mapperdd 
.dd 
Mapdd !
<dd! "
CommentReadDtodd" 0
>dd0 1
(dd1 2
resultdd2 8
)dd8 9
)dd9 :
;dd: ;
}ee 	
[pp 	 
ProducesResponseTypepp	 
(pp 
StatusCodespp )
.pp) *
Status200OKpp* 5
)pp5 6
]pp6 7
[qq 	 
ProducesResponseTypeqq	 
(qq 
StatusCodesqq )
.qq) *
Status400BadRequestqq* =
)qq= >
]qq> ?
[rr 	 
ProducesResponseTyperr	 
(rr 
StatusCodesrr )
.rr) *!
Status401Unauthorizedrr* ?
)rr? @
]rr@ A
[ss 	 
ProducesResponseTypess	 
(ss 
StatusCodesss )
.ss) *(
Status500InternalServerErrorss* F
)ssF G
]ssG H
[tt 	
HttpGettt	 
(tt 
$strtt 
)tt 
]tt  
publicuu 
ActionResultuu 
<uu 
CommentReadDtouu *
>uu* +
GetForAduu, 4
(uu4 5
intuu5 8
adIduu9 =
)uu= >
{vv 	
varww 
commentsww 
=ww 
_commentRepositoryww -
.ww- .
Getww. 1
(ww1 2
)ww2 3
.xx 
Wherexx 
(xx 
commentxx 
=>xx !
commentxx" )
.xx) *
AdIdxx* .
==xx/ 1
adIdxx2 6
)xx6 7
.xx7 8
ToListxx8 >
(xx> ?
)xx? @
;xx@ A
_loggeryy 
.yy 
Logyy 
(yy 
$stryy /
)yy/ 0
;yy0 1
returnzz 
Okzz 
(zz 
_mapperzz 
.zz 
Mapzz !
<zz! "
Listzz" &
<zz& '
CommentReadDtozz' 5
>zz5 6
>zz6 7
(zz7 8
commentszz8 @
)zz@ A
)zzA B
;zzB C
}{{ 	
[
ÜÜ 	"
ProducesResponseType
ÜÜ	 
(
ÜÜ 
StatusCodes
ÜÜ )
.
ÜÜ) *
Status200OK
ÜÜ* 5
)
ÜÜ5 6
]
ÜÜ6 7
[
áá 	"
ProducesResponseType
áá	 
(
áá 
StatusCodes
áá )
.
áá) *!
Status400BadRequest
áá* =
)
áá= >
]
áá> ?
[
àà 	"
ProducesResponseType
àà	 
(
àà 
StatusCodes
àà )
.
àà) *#
Status401Unauthorized
àà* ?
)
àà? @
]
àà@ A
[
ââ 	"
ProducesResponseType
ââ	 
(
ââ 
StatusCodes
ââ )
.
ââ) **
Status500InternalServerError
ââ* F
)
ââF G
]
ââG H
[
ää 	
HttpPut
ää	 
]
ää 
public
ãã 
ActionResult
ãã 
<
ãã 
CommentReadDto
ãã *
>
ãã* +
Update
ãã, 2
(
ãã2 3
CommentUpdateDto
ãã3 C
commentUpdateDto
ããD T
)
ããT U
{
åå 	
Comment
çç 
comment
çç 
=
çç  
_commentRepository
çç 0
.
çç0 1
Get
çç1 4
(
çç4 5
commentUpdateDto
çç5 E
.
ççE F
Id
ççF H
)
ççH I
;
ççI J
if
éé 
(
éé 
comment
éé 
==
éé 
null
éé 
)
éé  
{
èè 
return
êê 

BadRequest
êê !
(
êê! "
$str
êê" G
)
êêG H
;
êêH I
}
ëë 
comment
ìì 
=
ìì 
_mapper
ìì 
.
ìì 
Map
ìì !
(
ìì! "
commentUpdateDto
ìì" 2
,
ìì2 3
comment
ìì4 ;
)
ìì; <
;
ìì< = 
_commentRepository
îî 
.
îî 
Update
îî %
(
îî% &
comment
îî& -
)
îî- .
;
îî. /
_logger
ïï 
.
ïï 
Log
ïï 
(
ïï 
$str
ïï (
)
ïï( )
;
ïï) *
return
ññ 
Ok
ññ 
(
ññ 
comment
ññ 
)
ññ 
;
ññ 
}
óó 	
[
¢¢ 	"
ProducesResponseType
¢¢	 
(
¢¢ 
StatusCodes
¢¢ )
.
¢¢) *
Status200OK
¢¢* 5
)
¢¢5 6
]
¢¢6 7
[
££ 	"
ProducesResponseType
££	 
(
££ 
StatusCodes
££ )
.
££) *!
Status400BadRequest
££* =
)
££= >
]
££> ?
[
§§ 	"
ProducesResponseType
§§	 
(
§§ 
StatusCodes
§§ )
.
§§) *#
Status401Unauthorized
§§* ?
)
§§? @
]
§§@ A
[
•• 	"
ProducesResponseType
••	 
(
•• 
StatusCodes
•• )
.
••) **
Status500InternalServerError
••* F
)
••F G
]
••G H
[
¶¶ 	

HttpDelete
¶¶	 
(
¶¶ 
$str
¶¶ 
)
¶¶ 
]
¶¶ 
public
ßß 
ActionResult
ßß 
<
ßß 
CommentReadDto
ßß *
>
ßß* +
Delete
ßß, 2
(
ßß2 3
int
ßß3 6
id
ßß7 9
)
ßß9 :
{
®® 	
Comment
©© 
comment
©© 
=
©©  
_commentRepository
©© 0
.
©©0 1
Get
©©1 4
(
©©4 5
id
©©5 7
)
©©7 8
;
©©8 9
if
™™ 
(
™™ 
comment
™™ 
==
™™ 
null
™™ 
)
™™  
{
´´ 
return
¨¨ 

BadRequest
¨¨ !
(
¨¨! "
$str
¨¨" E
)
¨¨E F
;
¨¨F G
}
≠≠  
_commentRepository
ÆÆ 
.
ÆÆ 
Delete
ÆÆ %
(
ÆÆ% &
comment
ÆÆ& -
)
ÆÆ- .
;
ÆÆ. /
_logger
ØØ 
.
ØØ 
Log
ØØ 
(
ØØ 
$str
ØØ (
)
ØØ( )
;
ØØ) *
return
∞∞ 
Ok
∞∞ 
(
∞∞ 
)
∞∞ 
;
∞∞ 
}
±± 	
}
≤≤ 
}≥≥ 