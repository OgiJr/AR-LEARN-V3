# Обучение с обогатена реалност (AR LEARN)

https://github.com/OgiJr/AR-LEARN-V3

### Създатели:

Огнян Траянов, Американския Колеж, o.trajanov22@acsbg.org

Борис Радулов, Американския Колеж, b.radulov20@acsbg.org

## Идея на проекта:

Технологиите в днешно време са важна част от живота на всеки един човек. Информационните технологии са се развили във всяка една сфера на днешното общество, като почнем с транспорт, като самолети и автомобилите и стигнем до комуникацията ни. В образователната система обаче информационните технологии са назад със светлинни години и не са интегрирани в класните стаи и това трябва да се промени.

ООР ще помогне с интеграцията на информационните технологии в класната стая, като допринесе възможноста да се използва обогатена реалност (Augmented Reality) в часовете. Разширената реалност представлява комбинацията от данни от истинския свят с данни от виртуалния. Например нашето приложение използва изображението от камерата в истинския свят и добавя виртуални 3D изображения. 

Обогатената реалност ще даде възможноста на учениците да виждат материала, по-добре илюстриран, и допълнителна информация към него. Това помага на учениците с обучението им от класната стая и от вкъщи.

Един аргумент срещо интеграцията на информационните технологии е че ще струва много пари и ще изисква много ресурси. Тук идват в полза 
телефоните на учениците- средство, което до сега е виждано, като източник на мамене и разсейване може да се превърне в източник на знания и учение. Почти всеки ученик в XXIв. има достъп до телефон и до интернет, което му позволява да използва тази технология и да не струва на държавата пари да екипира училищата с технологии, като хромбуци.

## Сканиране на изображението:
Разширената реалност работи, като се сканира с камерата на телефона едно изображение и излезне 3D модел върху изображението. Този 3D модел е паралелен към изображението, като и двете са активни едновременно. Изображението е в реално време и може да бъде промерено. В нашето приложение, като се сканира изображението се подава заявка към сървъра, взима се името на изображението, ако бъде разпознато, и се дава на приложението, за да зареди специфичния обект. Това позволява на потребителя да сканира изображението и да получава резултат към разширената реалност. 

## Framework Дизайн:
Целта на проекта е потребителите сами да вкарват своите модели и изображения към базата данни. Това позволява нашето приложение да не бъде подходящо само за един учебник, а да бъде framework за всички учебници, който може да бъде използван от учители или издателства.  
3D Модели:
3D моделите се създават в някой 3D CAD софтуер или взимат от някое друго лице. Те после се добавят към сървъра и базата данни през уебсайта. Те могат да бъдат програмирани, анимирани, имат аудио и още много. Лимита на програмиране на потребителят е неограничен, защото формата на 3D модела е fbx, което подържа материали и анимации или Unity Prefab, което поддръжа модели, анимации и пргорами, както и всички видове компоненти в Unity, правейки го неограничено до една степен. 

## Изображения:
Изображенията към, които се прикачват 3D обектите се избират от потребителя спрямо ситуацията. Ако обектът е за учебник може да се сканира учебника, ако не е нужно да е учебник или книга може просто да се качи всякакво изображение. Изображението, колкото повече ръбове има, за да могат да се слагат markers, толкова по-лесно ще бъде разпознато. Обли обекти не помагат, а колажи са най-добри за сканиране. 
#Качване на пакет към уебсайта:
Качването на материал се осъществява през нашия уебсайт: https://arlearn.xyz/. Първо се избира името на пакета, като пакет е групата от различни обекти и тяхните изображения. После се избира описание на пакета, за да се знае за какво става въпрос. Пример за пакет може да бъде „Учебник по литература 7 клас“ или „Учебници по физика Просвета“. После към всеки обект се качат 3D модели, изображения и допълнителна информация към тях. За 3D моделите се избира дали потребителят иска да е fbx формат или Unity Prefab с fbx модел към него. После се избира изображение. Накрая се избира md файл, който служи за описание на обекта. 

## Употреба:
Нашето приложение може да влезне в употреба за учебници, като щом се сканира вътре материала излиза 3D модел, който го илюстрира. 
Приложението също така може да се използва за музеи, за да  се илюстрира матеирала.
Приложението също така може да бъде използвано в часовете;
Приложението може да бъде използвано за книги, за да се илюстрира съдържанието.
Приложението може да поддържа видео игри.
Приложението може да поддържа картини и други видове изкуство.
Може да се добавя допълнителна информация, както например има в Американския Колеж има информация за сградите или към визитната картичка на AR Learn.

## Инструкции:
1)	Учителите/издателството качва снимки и модели в базата данни през уебсайта ни.
2)	Учениците инсталират приложението през Android Store.
3)	Учениците теглят пакета през приложението ни.
4)	Учениците сканират изображението към модела.

## Стъпки на създаване на проекта:
1.	Създаване прототип в Unity (Огнян)

	.	 Създаване на interface в Android Studio (Борис)

	.	 Интегриране на прототипа в Android Studio (Огнян и Борис)

	.	 Създаване на база данни с моделите на софтуера (Огнян и Борис)

	.	 Създаване на примерен модел на учебник (Борис)

	.	 Създаване на примерен модел в музей (Огнян)

## Ниво на сложност:
Основни проблеми при реализацията на софтуера са основно архитектурни т.е. са трудности при структурирането на нужната софтуерна архитектура за реализацията на проекта. Трябва да бъде създадена облачна система, която да държи нужните модели за 3-измерните визуализации, която да буде достъпна до всякакви видове устройства. Тази система трябва да е достатъчно бърза и стабилна, че да може да бъде ползвана от големи количества ученици и учители едновременно. Друг голям парапет пред приложението е този на поддържане на различни устройства. Тъй като приложението трябва да поддържа максимално количество устройства, трябва да софтуера да бъде тестван на колкото се може повече телефони. Нивото на сложност на проекта е високо, защото изисква комбиниране на много различни технологии и модули, чиято координация е ключова за успеха на приложението.
Логическо и функционално решение на проблема:
Приложението се нуждае от база данни, която да го снабдява с нужните 3D модели. Това ще бъде постигнато чрез базите данни на Vuforia за изображенията и база данни с 3D модели създадена от нас. Мобилното приложение за Android написано в Android Studio, ще се свързва с този база данни чрез линкове кодирани в QR кодове. От там ще се свързва с моделите. Тези модели ще бъдат зареждани в Unity модула на приложението, който ще ги визуализира и ще контролира интерфейса на тяхните анимации. На фундаментално ниво, Android приложението действа като посредник между а) сървъра и Unity модула и б) Android телефона и неговата камера и Unity модула.

## Реализация:
За да реализираме този проект използваме Unity, Visual Studio, Vuforia и Android Studio. Имплементацията на Augmented Reality е в Unity и визуализира 3D моделите чрез Vuforia. Vuforia е приложението, с което се осъществява работата с AR системата. Тази програма разпознава изображенията, като тя съхранява всичките тях в своя база данни и поставя обектите във добавената реалност. В Android Studio се изработва interface-а на приложението за телефони с операциона система Android и другата част от неговите функционалности: интерфейс за сваляне на модели, информационни табла за моделите.

## Лиценз:
This project is licensed under the terms of the GNU GPLv3 License. For more information, see LICENSE.

## Ръководител:
Д-р Паулина Иванова Тодорова p.todorova@acsbg.org, 089 619 4957 Информатика и Информационни Технологии, АКС

## Библиография:
Неделчев, Никола- Питагорова Теорема; Физика Графика, София 2019 г. (Ученик от американския колеж)

Сайтът на Американския Колеж в София за източници за информация за сградите

Hoagland, John. “Circulatory System”, Sketchfab, Sketchfab, 1 Jan. 1968, sketchfab.com/3d-models/circulatory-system-4d9b279600264497958ac834eb1529a8.

Ringo3D, and 116 products. “Airplane.” Spleen 3D Models and Textures | TurboSquid.com, 6 Sept. 2015.

Orbis Agenda. “Solar System.” Unity Asset Store - The Best Assets for Game Making, assetstore.unity.com/packages/3d/environments/sci-fi/solar-system-24810.

Maxegy. “KING Tutankhamun Mask | 3D Model.” CGTrader, CGTrader, www.cgtrader.com/3d-models/character/other/king-tutankhamun-mask.

MargetaCG. “Saturn V | 3D Model.” CGTrader, CGTrader, www.cgtrader.com/free-3d-models/aircraft/historic/saturn-v-1479cb7d-b978-42b4-90d9-f3e844e6ec55

System-IntegraTech, “Biology Cell Package” Unity Asset Store, https://assetstore.unity.com/packages/3d/biology-cells-pack-97118
