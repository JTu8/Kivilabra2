   M S B A M L   `   `   `  ����Q  MKivi- ja Massaseulonta, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:KiviMassaAppKiviMassaApp  5   	       KiviMassaApp.Massaohjelma   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:KiviMassaApp     5   	   -   5          �Name$  massaohjelma��5   	    � Title$ Massaohjelma��5   	   $	��828��6   $��1189.5��6+    � WindowState$ 	Maximized=�5	   	    � Topmost .  J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$
 Open��5      6I     5   
   -   5        $
 Save��5      6I    J� InputBindings 5      �� 5   
   
 �� Key$ O��6    �� 	Modifiers$
 Ctrlf�6   	 �� Command$
	 Open��6.   6?   �� 5   
   $ S��6   $
 Ctrlf�6   $
	 Save��6.   6?   �� 5   
   $ P��6   $
 Ctrlf�6   $	 Print��6.   6@   .��5      �� $��20��6   

 �� Top$

 -262��6   $	��370��6+    �� Left$ -6��67   $��0,0,1552,1045q�6H   .+�5   
   ]� .+�5      o� $	��Top=�6   .@�5      m� $��Tiedosto��6   ��.  .@�5      m� -   5         m�Name$ 	SaveFiles��5      $��Tallenna��62    m� Command$
 Save��6D    m� InputGestureText$ Ctrl + S��6S   6q   m� 5      -   5        $ 	CreatePDF��5      $��Luo PDF��62   6]   m� 5      -   5        $ ExitProgram��5      $��Lopeta��64   6`   5!      m� 5"      -   5        $��lataa��5"      $��Lataa��6(   ��.  .@�5#      m� -   5        $ OpenFileExplorer��5#   1   
 � Tag$ lataaKaikki��6   $��Lataa kaikki��6K   $ Ctrl + O��6a   6�   m� 5$      -	   5        $ 	OpenSeula��5$   0   $ 
lataaSeula��6   $��Lataa seula-arvot��6C   6y   m� 5%      -
   5        $ OpenOhje��5%   /   $ 	lataaOhje��6   $��Lataa ohjearvot��6A   6t   m� 5&      -   5        $ 
OpenTeksti��5&   1   $ lataaTiedot��6   $��Lataa tiedot��6E   6w   5'      m� 5(      $
��Ohje��6   ��.  5*      5+      5,      �� 5/   
   -   5        $��
tietoArvot��5/   N   $	��176��6   $
 23��6   $	��718��6.   
�������.+�50      �� $��Sekoitusasema:��6   6/   �� 51      -   5        $��sekoitusAsema��51      $ 96��6+   $��21��6<   $	��145��6H   $
 5��6T   $��1��6c   �� 53      $��Lisätietoja:��6   $
 26��6+   6=   �� 54      -   5        $��lisatietoja��54      $ 96��6)   $��21��6:   $	��145��6F   $
 31��6R   $��2��6b   �� 56      $��Alempi otsikko:��6   $
 52��6.   �� 57      -   5        $��alempiOtsikko��57      $��21��6+   $	��145��67   $ 96��6C   $
 57��6T   ���	#FFDBFAFF$��3��6{   6�   �� 59      $��Lähiosoite:��6   $
 78��6*   6<   �� 5:      -   5        $��
lahiOsoite��5:      $��21��6(   $	��145��64   $ 96��6@   $
 83��6Q   ���	#FFDBFAFF$��4��6x   6�   �� 5<      $��Osoite:��6   $	
 104��6&   69   �� 5=      -   5        $��osoite��5=      $��21��6$   $	��145��60   $ 96��6<   $	
 109��6M   ���	#FFDBFAFF$��5��6u   6�   �� 5?      $
��Puh:��6   $	
 130��6#   66   �� 5@      -   5        $	��puh��5@      $��21��6!   $	��145��6-   $ 96��69   $	
 135��6J   ���	#FFDBFAFF$��6��6r   6�   �� 5B      $��Päällyste:��6   $	 265��6)   $
 1��6;   6L   �� 5C      -   5        $��	paallyste��5C      $	 346��6'   $��21��69   $	��121��6E   $
 6��6Q   $��7��6`   �� 5E      $��
Päiväys:��6   $	 265��6'   $
 26��69   6K   �� 5F      -   5        $��paivays��5F      $	 346��6%   $��21��67   $	��121��6C   $
 32��6O   $��8��6_   �� 5H      $��Urakka:��6   $	 265��6&   $
 52��68   6J   �� 5I      -   5        $��urakka��5I      $	 346��6$   $��21��66   $	��121��6B   $
 58��6N   $��9��6^   �� 5K      $��
Työkohde:��6   $	 266��6(   $
 78��6:   6L   �� 5L      -   5        $��tyokohde��5L      $	 346��6&   $��21��68   $	��121��6D   $
 83��6P   $��10��6`   �� 5N      $��Näyte nro:��6   $	 482��6)   $
 1��6;   6L   �� 5O      -   5        $��nayteNro��5O      $	 586��6&   $��21��68   $	��121��6D   $
 5��6P   $��11��6_   �� 5Q      $
��Klo:��6   $	 482��6#   $
 27��65   6G   �� 5R      -   5        $	��klo��5R      $	 586��6!   $��21��63   $	��121��6?   $
 31��6K   $��12��6[   �� 5T      $��Paalu/kaista:��6   $	 482��6,   $
 53��6>   6P   �� 5U      -   5        $��paaluKaista��5U      $	 586��6)   $��21��6;   $	��121��6G   $
 57��6S   $��13��6c   �� 5W      $��Näytteen ottaja:��6   $	 482��6/   $
 78��6A   6S   �� 5Y      -   5        $��naytteenOttaja��5Y      $	 586��6,   $��21��6>   $	��121��6J   $
 83��6V   $��14��6f   �� 5[      $��Tutkija:��6    J� RenderTransformOrigin$ 0.896,0.769&�6'   $	 482��6K   $	
 104��6]   6p   �� 5\      -   5        $��tutkija��5\      $��21��6%   $	��121��61   $	 586��6=   $	
 109��6O   $��15��6`   6p   �� 5^      $��Valitse logo:��6   $	 482��6,   $	
 133��6>   6Q   �� 5_      -   5        $��Kuvat��5_   F   $	 586��6   $	
 136��6)   $	��120��6:   ��.  .@�5`      �� -   5        $��Kuva1��5`   7   $��Savonialogo 1��6    �� 
IsSelected . $. (.\Asetukset\kuvat\logot\savonialogo1.jpg��6V   6�   �� 5a      -   5        $��Kuva5��5a   7   $��Savonialogo 2��6   $. (.\Asetukset\kuvat\logot\savonialogo5.jpg��6D   6u   �� 5b      -   5        $��Kuva6��5b   <   $��Savonialogo 3 engl��6   $. (.\Asetukset\kuvat\logot\savonialogo6.jpg��6I   6z   5c      �� 5e      -    5        $��tallennaOsoitetiedot��5e   5   $��Tallenna osoitetiedot��6   $	 250��6Q   $	
 130��6c   $	��120��6�   $��26��6�   ���	#FFDBFAFF��.  6�   5f      �� 5i   
   -!   5        $��sideainelaskuri��5i   _   $	��176��6   $	 718��6   $
 23��60   $	��463��6@   
�������.+�5j      �� $��Sideainepitoisuus��6   $��41��60   $	��166��6<   $��20(�6H   $	 141��6V   
��� ���$
 -9��6|   6�   �� 5l      $"��Koko näytteen paino g (M1):��6   $
 28��6:   $ -3��6J   6]   �� 5m      -"   5        $��naytteenPaino��5m      $	 154��6+   $��21��6=   $��60��6I   $
 30��6T   $��20��6�   �� 5o      $��Kiviaineksen paino g:��6   $
 85��64   $ 0.467,0.115&�6D   �� 5p      -#   5         ��Name$ nayte��5p      $��21��6&   $	 154��62   $
 90��6D   $��60��6T    �� 
IsReadOnly . $��21��6�   
���������.  ��6�   Z� 6�   �� 5r      $��Rumpu + näyte g (M2):��6   $
 55��64   $ 0.467,0.115&�6D   �� 5s      -$   5        $��rumpujanayte��5s      $	 154��6*   $��21��6<   $��60��6H   $
 60��6S   $��22��6�   �� 5u      $��Sideainemäärä g:��6   $	
 115��6/   6B   �� 5v      -%   5        $��sideainemaara��5v      $	 154��6+   $��21��6=   $��60��6I   $	
 120��6T    . $��23��6�   ��.  
���������6�   Z� �� 5x      $��Sideainepitoisuus %:��6   $	
 145��63   6F   �� 5y      -&   5        $��sideainepitoisuus��5y      $	 154��6/   $��21��6A   $��60��6M   $	
 150��6X    . $��24��6�   ��.  
���������6�   Z� �� 5{      $��Rummun paino g (R):��6   $
 25��62   $ 0.467,0.115&�6B   $	 228��6f   6z   �� 5|      -'   5        $ rummunPaino��5|      $��21��6,   $	 391��68   $
 30��6J   $��60��6Z   $ 1.267,0.619&�6�   $��25��6�   �� 5~      $#��Sentrifuugi + paperi g (sf1):��6   $
 55��6<   $ 0.467,0.115&�6L   $	 228��6p   6�   �� 5      -(   5        $ sentrifuugipaperi��5      $��21��61   $	 391��6=   $
 60��6O   $��60��6_   $��26��6�   �� 5�      $&�� Sentrif.+paperi+filleri g (sf2):��6   $
 85��6?   $ 0.467,0.115&�6O   $	 228��6s   6�   �� 5�      -)   5        $ sentrifuugipaperirilleri��5�      $��21��68   $	 391��6D   $
 90��6V   $��60��6f   $ 
1.417,0.19&�6�   $��27��6�   �� 5�      $��Fillerin paino g (F):��6   $	
 115��64   $ 0.467,0.115&�6E   $	 228��6i   6}   �� 5�      -*   5        $ Filleri��5�      $��21��6(   $	 391��64   $	
 120��6F   $��60��6W    . $��28��6�   ��.  
���������6�   Z� �� 5�      -+   5         ��Name$ btnLaskeSideainepitoisuus��5�      $	 376��68   $	
 145��6J   $��75��6[   $��Laske��6�   $��29��6�   $��26��6�   �� 5�      -,   5        $ Tyhjennasideaine��5�      $��26��6/   $��67��6;   $	 283��6F   $	
 145��6X   ���	#FFE05A5A���	#FF400101
���   �$��14(�6�   $��	Tyhjennä��6�   ��.  6�   5�      �� 5�   
   --   5        $��
seulaArvot��5�   W   $	��599��6   $	
 199��6   $	��718��6/   
�������.+�5�      �� -.   5        $��Seula1��5�      $��0��6%   $��1��65   $
 62��6B   $��75��6R   $	 100��6]   $ seula��6o   ��.  5�      �� 5�      -/   5        $��Seula2��5�      $��0��6%   $��1��65   $
 89��6B   $��21��6R   $��75��6^   $	 100��6i   $ seula��6{   ��.  5�      �� 5�      -0   5        $��Seula3��5�      $��0��6%   $��1��65   $	
 115��6B   $��21��6S   $��75��6_   $	 100��6j   $ seula��6|   ��.  5�      �� 5�      -1   5        $��Seula4��5�      $��0��6%   $��1��65   $	
 141��6B   $��21��6S   $��75��6_   $	 100��6j   $ seula��6|   ��.  5�      �� 5�      -2   5        $��Seula5��5�      $��0��6%   $��1��65   $��21��6B   $��75��6N   $	
 167��6Y   $	 100��6j   $ seula��6|   ��.  5�      �� 5�      -3   5        $��Seula6��5�      $��0��6%   $��1��65   $��21��6B   $��75��6N   $	
 193��6Y   $	 100��6j   $ seula��6|   ��.  5�      �� 5�      -4   5        $��Seula7��5�      $��0��6%   $��1��65   $��21��6B   $��75��6N   $	
 219��6Y   $	 100��6j   $ seula��6|   ��.  5�      �� 5�      -5   5        $��Seula8��5�      $��0��6%   $��1��65   $��21��6B   $��75��6N   $	
 245��6Y   $	 100��6j   $ seula��6|   ��.  5�      �� 5�      -6   5        $��Seula9��5�      $��0��6%   $��1��65   $��21��6B   $��75��6N   $	
 271��6Y   $	 100��6j   $ seula��6|   ��.  5�      �� 5�      -7   5        $��Seula10��5�      $��0��6&   $��1��66   $��21��6C   $��75��6O   $	
 297��6Z   $	 100��6k   $ seula��6}   ��.  5�      �� 5�      -8   5        $��Seula11��5�      $��0��6&   $��1��66   $��21��6C   $��75��6O   $	
 323��6Z   $	 100��6k   $ seula��6}   ��.  5�      �� 5�      -9   5        $��Seula12��5�      $��0��6&   $��1��66   $��21��6C   $��75��6O   $	
 349��6Z   $	 100��6k   $ seula��6}   ��.  5�      �� 5�      -:   5        $��Seula13��5�      $��0��6&   $��1��66   $��21��6C   $��75��6O   $	
 375��6Z   $	 100��6k   $ seula��6}   ��.  5�      �� 5�      -;   5        $��Seula14��5�      $��0��6&   $��1��66   $��21��6C   $��75��6O   $	
 401��6Z   $	 100��6k   $ seula��6}   ��.  5�      �� 5�      -<   5        $��Seula15��5�      $��0��6&   $��1��66   $��21��6C   $��75��6O   $	
 427��6Z   $	 100��6k   $ seula��6}   ��.  5�      �� 5�      -=   5        $��Seula16��5�      $��0��6&   $��1��66   $��21��6C   $��75��6O   $	
 453��6Z   $	 100��6k   $ seula��6}   ��.  5�      �� 5�      $��Pohja��6   $	 110��6$   $	
 479��66   6I   �� 5�      ->   5        $��seulaG0��5�      $��21��6%   $��75��61   $	 225��6<   $
 62��6N   $
 arvo��6�   $��30��6�   6�   �� 5�      -?   5        $��seulaG1��5�      $��21��6%   $��75��61   $	 225��6<   $
 88��6N   $
 arvo��6�   $��31��6�   6�   �� 5�      -@   5        $��seulaG2��5�      $��21��6%   $��75��61   $	 225��6<   $	
 114��6N   $
 arvo��6�   $��32��6�   6�   �� 5�      -A   5        $��seulaG3��5�      $��21��6%   $��75��61   $	 225��6<   $	
 140��6N   $
 arvo��6�   $��33��6�   6�   �� 5�      -B   5        $��seulaG4��5�      $��21��6%   $��75��61   $	 225��6<   $	
 166��6N   $
 arvo��6�   $��34��6�   6�   �� 5�      -C   5        $��seulaG5��5�      $��21��6%   $��75��61   $	 225��6<   $	
 192��6N   $
 arvo��6�   $��35��6�   6�   �� 5�      -D   5        $��seulaG6��5�      $��21��6%   $��75��61   $	 225��6<   $	
 218��6N   $
 arvo��6�   $��36��6�   6�   �� 5�      -E   5        $��seulaG7��5�      $��21��6%   $��75��61   $	 225��6<   $	
 244��6N   $
 arvo��6�   $��37��6�   6�   �� 5�      -F   5        $��seulaG8��5�      $��21��6%   $��75��61   $	 225��6<   $	
 270��6N   $
 arvo��6�   $��38��6�   6�   �� 5�      -G   5        $��seulaG9��5�      $��21��6%   $��75��61   $	 225��6<   $	
 296��6N   $
 arvo��6�   $��39��6�   6�   �� 5�      -H   5        $��seulaG10��5�      $��21��6&   $��75��62   $	 225��6=   $	
 322��6O   $
 arvo��6�   $��40��6�   6�   �� 5�      -I   5        $��seulaG11��5�      $��21��6&   $��75��62   $	 225��6=   $	
 348��6O   $
 arvo��6�   $��41��6�   6�   �� 5�      -J   5        $��seulaG12��5�      $��21��6&   $��75��62   $	 225��6=   $	
 374��6O   $
 arvo��6�   $��42��6�   6�   �� 5�      -K   5        $��seulaG13��5�      $��21��6&   $��75��62   $	 225��6=   $	
 400��6O   $
 arvo��6�   $��43��6�   6�   �� 5�      -L   5        $��seulaG14��5�      $��21��6&   $��75��62   $	 225��6=   $	
 426��6O   $
 arvo��6�   $��44��6�   6�   �� 5�      -M   5        $��seulaG15��5�      $��21��6&   $��75��62   $	 225��6=   $	
 452��6O   $
 arvo��6�   $��45��6�   6�   �� 5�      -N   5        $��seulaG16��5�      $��21��6&   $��75��62   $	 225��6=   $	
 478��6O   $
 arvo��6�   $��46��6�   6�   �� 5�      -O   5        $��
seulapros1��5�      $��21��6(   $��75��64   $	 350��6?   $
 62��6Q    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -P   5        $��
seulapros2��5�      $��21��6(   $��75��64   $	 350��6?   $
 88��6Q    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -Q   5        $��
seulapros3��5�      $��21��6(   $��75��64   $	 350��6?   $	
 114��6Q    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -R   5        $��
seulapros4��5�      $��21��6(   $��75��64   $	 350��6?   $	
 140��6Q    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -S   5        $��
seulapros5��5�      $��21��6(   $��75��64   $	 350��6?   $	
 166��6Q    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -T   5        $��
seulapros6��5�      $��21��6(   $��75��64   $	 350��6?   $	
 192��6Q    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -U   5        $��
seulapros7��5�      $��21��6(   $��75��64   $	 350��6?   $	
 218��6Q    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -V   5        $��
seulapros8��5�      $��21��6(   $��75��64   $	 350��6?   $	
 244��6Q    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -W   5        $��
seulapros9��5�      $��21��6(   $��75��64   $	 350��6?   $	
 270��6Q    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -X   5        $��seulapros10��5�      $��21��6)   $��75��65   $	 350��6@   $	
 296��6R    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -Y   5        $��seulapros11��5�      $��21��6)   $��75��65   $	 350��6@   $	
 322��6R    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -Z   5        $��seulapros12��5�      $��21��6)   $��75��65   $	 350��6@   $	
 348��6R    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -[   5        $��seulapros13��5�      $��21��6)   $��75��65   $	 350��6@   $	
 374��6R    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -\   5        $��seulapros14��5�      $��21��6)   $��75��65   $	 350��6@   $	
 400��6R    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -]   5        $��seulapros15��5�      $��21��6)   $��75��65   $	 350��6@   $	
 426��6R    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -^   5        $��seulapros16��5�      $��21��6)   $��75��65   $	 350��6@   $	
 452��6R    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -_   5        $��seulapros17��5�      $��21��6)   $��75��65   $	 350��6@   $	
 478��6R    . 
����� �$ tulos1��6�   ��.  6�   �� 5�      -`   5        $��lapaisypros1��5�      $��21��6*   $��75��66   $	 475��6A   $
 62��6S    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -a   5        $��lapaisypros2��5�      $��21��6*   $��75��66   $	 475��6A   $
 88��6S    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -b   5        $��lapaisypros3��5�      $��21��6*   $��75��66   $	 475��6A   $	
 114��6S    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -c   5        $��lapaisypros4��5�      $��21��6*   $��75��66   $	 475��6A   $	
 140��6S    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -d   5        $��lapaisypros5��5�      $��21��6*   $��75��66   $	 475��6A   $	
 166��6S    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -e   5        $��lapaisypros6��5�      $��21��6*   $��75��66   $	 475��6A   $	
 192��6S    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -f   5        $��lapaisypros7��5�      $��21��6*   $��75��66   $	 475��6A   $	
 218��6S    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -g   5        $��lapaisypros8��5�      $��21��6*   $��75��66   $	 475��6A   $	
 244��6S    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -h   5        $��lapaisypros9��5�      $��21��6*   $��75��66   $	 475��6A   $	
 270��6S    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -i   5        $��lapaisypros10��5�      $��21��6+   $��75��67   $	 475��6B   $	
 296��6T    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -j   5        $��lapaisypros11��5�      $��21��6+   $��75��67   $	 475��6B   $	
 322��6T    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -k   5        $��lapaisypros12��5�      $��21��6+   $��75��67   $	 475��6B   $	
 348��6T    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -l   5        $��lapaisypros13��5�      $��21��6+   $��75��67   $	 475��6B   $	
 374��6T    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -m   5        $��lapaisypros14��5�      $��21��6+   $��75��67   $	 475��6B   $	
 400��6T    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -n   5        $��lapaisypros15��5�      $��21��6+   $��75��67   $	 475��6B   $	
 426��6T    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -o   5        $��lapaisypros16��5�      $��21��6+   $��75��67   $	 475��6B   $	
 452��6T    . ���#ffccb3$ tulos2��6�   ��.  6�   �� 5�      -p   5         ��Name$ lbMarkaPaino��5�      $��Märkäpaino g:��6*   $	 597��6B   $
 53��6T   6f   �� 5�      -q   5        $��tbMarkaPaino��5�      $��21��6*   $��76��66   $
 79��6A   $	 602��6Q   $ 
markapaino��6c   $��47��6t   6�   �� 5�      -r   5        $ 
lbKosteusP��5�      $��
Kosteus -%��6(   $	
 110��6=   $	 605��6N   6b   �� 5�      -s   5        $��
tbKosteusP��5�      $��21��6(   $��76��64   $	 602��6?   $	
 136��6Q    . $ kosteuspros��6�   ��.  ��6t   Z� 6�   �� 5�      -t   5        $ btnSeulaLaske��5�      $��53��6,   $��94��68   $	 588��6C   $	
 523��6U   ���	#FFD0FF8B���	#FF2F8100$��16(�6�   $ 0.298,0.283&�6�   $��48��6�   .��6�   Laske6�   �� 5�      -u   5        $ EmptyValues��5�      $��53��6*   $��94��66   $ 69��6A   $	
 523��6R   ���	#FFE05A5A���	#FF400101
���   �$��14(�6�   $��Tyhjää arvot��6�   ��.  6�   �� 5�      -v   5        $ lblDesimaali��5�      $	 585��6*   $	
 390��6<   .��5�      ��  �� TextAlignment$ Center=�5�      $&�� Tulosten tarkkuus
desimaaleissa:��5�      $��92��6A   5�      �� 5�      -w   5         ��Name$ dbDesimaali��5�      $	
 438��6,   $��51��6=   $	 611��6H   $��26��6Z   $ 	desimaali��6f   $��47��6v   ��.  .@�5�      ��  . $ 1��61   $��1��69   �� 5�      $ 2��6   $��2��6'   �� 5�      $ 3��6   $��3��6'   5�      �� 5�      $��Punnittu yht. (g):��6   $	 275��61   $	
 534��6C   $��14(�6T   �� 5�      -x   5        $ punnittuYhteensa��5�      $��23��60   $��68��6<   $	 385��6G   $	
 538��6Y    . $ yhteispaino��6|   ��.  ��6�   Z� �� 5�      $��Seula��6   $
��Left=�6$   $	��Top=�6?   ���	#FFFFB374���	#FFB80202$	��125��6�   $��14(�6�   $	 100��6�   �� 5�      $�� ��6   $
��Left=�6   $	��Top=�6:   ���	#FFFFB374���	#FFB80202$	��100��6�   $��14(�6�   �� 5�      $��Seulalle jäi (g)��6   $	 225��6/   ���	#FFFFB374���	#FFB80202$	��125��6o   $��14(�6{   �� 5�      $��Seulalle jäi %��6   $	 350��6-   ���	#FFB80202���	#FFFFB374$	��125��6m   $��14(�6y   �� 5�      $��Läpäisy %��6   $	 475��6(   ���	#FFFFB274���	#FFB80202$	��243��6h   $��14(�6t   5     �� 5  
   -y   5        $��ohjeAlue��5  `   $	��599��6   $	 718��6   $	
 199��60   $	��463��6A   
�������.+�5     �� $��	Seula #mm��6   $ 92��6(   $
 21��69   $ 
seulaValue��6I   6\   �� 5     -z   5        $��seulaValue1��5     $��21��6)   $��68��65   $
 53��6@    . $ 94��6b   $ 
seulaValue��6s   ��.  ��6�   Z� 6�   �� 5     -{   5        $��seulaValue2��5     $��21��6)   $��68��65   $
 79��6@    . $ 94��6b   $ 
seulaValue��6s   ��.  ��6�   Z� 6�   �� 5     -|   5        $��seulaValue3��5     $��21��6)   $��68��65   $	
 105��6@    . $ 94��6c   $ 
seulaValue��6t   ��.  ��6�   Z� 6�   �� 5	     -}   5        $��seulaValue4��5	     $��21��6)   $��68��65   $	
 131��6@    . $ 94��6c   $ 
seulaValue��6t   ��.  ��6�   Z� 6�   �� 5
     -~   5        $��seulaValue5��5
     $��21��6)   $��68��65   $	
 157��6@    . $ 94��6c   $ 
seulaValue��6t   ��.  ��6�   Z� 6�   �� 5     -   5        $��seulaValue6��5     $��21��6)   $��68��65   $	
 183��6@    . $ 94��6c   $ 
seulaValue��6t   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue7��5     $��21��6)   $��68��65   $	
 209��6@    . $ 94��6c   $ 
seulaValue��6t   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue8��5     $��21��6)   $��68��65   $	
 235��6@    . $ 94��6c   $ 
seulaValue��6t   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue9��5     $��21��6)   $��68��65   $	
 261��6@    . $ 94��6c   $ 
seulaValue��6t   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue10��5     $��21��6*   $��68��66   $	
 287��6A    . $ 94��6d   $ 
seulaValue��6u   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue11��5     $��21��6*   $��68��66   $	
 313��6A    . $ 94��6d   $ 
seulaValue��6u   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue12��5     $��21��6*   $��68��66   $	
 339��6A    . $ 94��6d   $ 
seulaValue��6u   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue13��5     $��21��6*   $��68��66   $	
 365��6A    . $ 94��6d   $ 
seulaValue��6u   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue14��5     $��21��6*   $��68��66   $	
 391��6A    . $ 94��6d   $ 
seulaValue��6u   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue15��5     $��21��6*   $��68��66   $	
 417��6A    . $ 94��6d   $ 
seulaValue��6u   ��.  ��6�   Z� 6�   �� 5     -�   5        $��seulaValue16��5     $��21��6*   $��68��66   $	
 443��6A    . $ 94��6d   $ 
seulaValue��6u   ��.  ��6�   Z� 6�   �� 5     $��Alaraja��6   $	 214��6&   $
 21��68   6J   �� 5     -�   5        $��alaRajaValue1��5     $��21��6+   $��68��67   $
 53��6B   $	 204��6R   $	 Ala��6�   $��49��6�   6�   �� 5     -�   5        $��alaRajaValue2��5     $��21��6+   $��68��67   $
 79��6B   $	 204��6R   $	 Ala��6�   $��50��6�   6�   �� 5     -�   5        $��alaRajaValue3��5     $��21��6+   $��68��67   $	
 105��6B   $	 204��6S   $	 Ala��6�   $��51��6�   6�   �� 5     -�   5        $��alaRajaValue4��5     $��21��6+   $��68��67   $	
 131��6B   $	 204��6S   $	 Ala��6�   $��52��6�   6�   �� 5     -�   5        $��alaRajaValue5��5     $��21��6+   $��68��67   $	
 157��6B   $	 204��6S   $	 Ala��6�   $��53��6�   6�   �� 5     -�   5        $��alaRajaValue6��5     $��21��6+   $��68��67   $	
 183��6B   $	 204��6S   $	 Ala��6�   $��54��6�   6�   �� 5     -�   5        $��alaRajaValue7��5     $��21��6+   $��68��67   $	
 209��6B   $	 204��6S   $	 Ala��6�   $��55��6�   6�   �� 5      -�   5        $��alaRajaValue8��5      $��21��6+   $��68��67   $	
 235��6B   $	 204��6S   $	 Ala��6�   $��56��6�   6�   �� 5!     -�   5        $��alaRajaValue9��5!     $��21��6+   $��68��67   $	
 261��6B   $	 204��6S   $	 Ala��6�   $��57��6�   6�   �� 5"     -�   5        $��alaRajaValue10��5"     $��21��6,   $��68��68   $	
 287��6C   $	 204��6T   $	 Ala��6�   $��58��6�   6�   �� 5#     -�   5        $��alaRajaValue11��5#     $��21��6,   $��68��68   $	
 313��6C   $	 204��6T   $	 Ala��6�   $��59��6�   6�   �� 5$     -�   5        $��alaRajaValue12��5$     $��21��6,   $��68��68   $	
 339��6C   $	 204��6T   $	 Ala��6�   $��60��6�   6�   �� 5%     -�   5        $��alaRajaValue13��5%     $��21��6,   $��68��68   $	
 365��6C   $	 204��6T   $	 Ala��6�   $��61��6�   �� 5&     -�   5        $��alaRajaValue14��5&     $��21��6,   $��68��68   $	
 391��6C   $	 204��6T   $	 Ala��6�   $��62��6�   6�   �� 5'     -�   5        $��alaRajaValue15��5'     $��21��6,   $��68��68   $	
 417��6C   $	 204��6T   $	 Ala��6�   $��63��6�   6�   �� 5(     -�   5        $��alaRajaValue16��5(     $��21��6,   $��68��68   $	
 443��6C   $	 204��6T   $	 Ala��6�   $��64��6�   6�   �� 5+     $��Yläraja��6   $	 296��6&   $
 21��68   6J   �� 5,     -�   5        $��ylaRajaValue1��5,     $��21��6+   $��68��67   $
 53��6B   $	 288��6R   $	 Yla��6�   $��65��6�   6�   �� 5-     -�   5        $��ylaRajaValue2��5-     $��21��6+   $��68��67   $
 79��6B   $	 288��6R   $	 Yla��6�   $��66��6�   6�   �� 5.     -�   5        $��ylaRajaValue3��5.     $��21��6+   $��68��67   $	
 105��6B   $	 288��6S   $	 Yla��6�   $��67��6�   6�   �� 5/     -�   5        $��ylaRajaValue4��5/     $��21��6+   $��68��67   $	
 131��6B   $	 288��6S   $	 Yla��6�   $��68��6�   6�   �� 50     -�   5        $��ylaRajaValue5��50     $��21��6+   $��68��67   $	
 157��6B   $	 288��6S   $	 Yla��6�   $��69��6�   6�   �� 51     -�   5        $��ylaRajaValue6��51     $��21��6+   $��68��67   $	
 183��6B   $	 288��6S   $	 Yla��6�   $��70��6�   6�   �� 52     -�   5        $��ylaRajaValue7��52     $��21��6+   $��68��67   $	
 209��6B   $	 288��6S   $	 Yla��6�   $��71��6�   6�   �� 53     -�   5        $��ylaRajaValue8��53     $��21��6+   $��68��67   $	
 235��6B   $	 288��6S   $	 Yla��6�   $��72��6�   6�   �� 54     -�   5        $��ylaRajaValue9��54     $��21��6+   $��68��67   $	
 261��6B   $	 288��6S   $	 Yla��6�   $��73��6�   6�   �� 55     -�   5        $��ylaRajaValue10��55     $��21��6,   $��68��68   $	
 287��6C   $	 288��6T   $	 Yla��6�   $��74��6�   6�   �� 56     -�   5        $��ylaRajaValue11��56     $��21��6,   $��68��68   $	
 313��6C   $	 288��6T   $	 Yla��6�   $��75��6�   6�   �� 57     -�   5        $��ylaRajaValue12��57     $��21��6,   $��68��68   $	
 339��6C   $	 288��6T   $	 Yla��6�   $��76��6�   6�   �� 58     -�   5        $��ylaRajaValue13��58     $��21��6,   $��68��68   $	
 365��6C   $	 288��6T   $	 Yla��6�   $��77��6�   6�   �� 59     -�   5        $��ylaRajaValue14��59     $��21��6,   $��68��68   $	
 391��6C   $	 288��6T   $	 Yla��6�   $��78��6�   6�   �� 5:     -�   5        $��ylaRajaValue15��5:     $��21��6,   $��68��68   $	
 417��6C   $	 288��6T   $	 Yla��6�   $��79��6�   6�   �� 5;     -�   5        $��ylaRajaValue16��5;     $��21��6,   $��68��68   $	
 443��6C   $	 288��6T   $	 Yla��6�   $��80��6�   6�   �� 5>     -�   5        $ btnTyhjennaaOhjeAlue��5>     $��32��63   $	��162��6?   $	 254��6K   $	
 494��6]   ��.  ���	#FFE05A5A���	#FF400101.��6�   Tyhjennä ohjealue6�   �� 5?     -�   5        $ btnNaytaKaavio��5?     $��Näytä rakeisuuskäyrä��6-   $ 50��6L   $	
 494��6]   $	��162��6n   $��32��6z   $��16(�6�   ���	#FFD0FF8B���	#FF2F8100$��82��6�   5@     5B     5C     