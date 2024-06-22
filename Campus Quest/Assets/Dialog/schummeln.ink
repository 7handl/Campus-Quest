-> main

=== main ===
Ich bin der mysteriöse Dr.XXXXX!
Bist du bereit dich meiner Prüfung zu stellen?


    *[Ja!]
        Niemand ist bereit für meine Prüfung!
    -> FrageSchummeln
    
    *[Nein.]
        Das ist die richtige Einstellung!
    -> FrageSchummeln
    
    
=== FrageSchummeln ===
Meine Prüfung ist anders als Alle, die du gewohnt bist.
    * [Aber warum?]
        Hör zu, ich werde es dir erklären.
        -> erklaerung
        
    * [Ich werde trotzdem bestehen!]
        Bist du dir da sicher?
        -> erklaerung
        
    * [Ich bin verwirrt.]
        Das bin ich auch!
        ->FrageSchummeln
        

=== erklaerung ===
Die Regeln meiner Prüfung befinden sich in einem stetigen Wandel.
*[Was bedeutet das?]
    Das sie sich auch einfach während der Prüfung ändern können,
    Du hast keine Chance sie zu umgehen!
    
    *[Ich werde es trotzdem versuchen!]
        Es wird dir nicht gelingen!
        -> END
        
    *[Ich werde fair bestehen!]
        Falsche Antwort!
        ->erklaerung
        
    *[Das verstehe ich nicht.]
        Ich auch nicht.
        ->erklaerung
        
    
        
        




Die Antworten zu kennen, ist nur ein Teil der Prüfung. Meine Prüfung testet auch dein Glück!

Lukas: (verärgert) Das klingt wie ein Rätsel! Wie soll ich das alles auf einmal beweisen?

Herr Schatten: (schrittweise näherkommend) Es ist ein Rätsel, ja. Aber eins, das du lösen kannst. Doch nicht durch Schummeln. Jede Antwort, die du aufschreibst, wird durch deine wahre Absicht gefärbt.

Lukas: (blickt auf seine Hände) Also, was muss ich tun?

Herr Schatten: (mit einem geheimnisvollen Lächeln) Finde die Wahrheit in dir selbst. Die Antworten, die du suchst, sind nicht in Büchern oder bei deinen Mitschülern. Sie sind in deinem Herzen.


Herr Schatten: (legt eine Hand auf Lukas' Schulter) Du kannst es nur versuchen. Aber ich verspreche dir, Lukas, wenn du deinen wahren Weg findest, wirst du die Prüfung bestehen. Und dann wirst du verstehen, dass Schummeln nie eine Option war.

Lukas: (entschlossen) Ich werde es versuchen, Herr Schatten. Danke.

Herr Schatten: (lächelnd) Gute Wahl, Lukas. Möge dein Herz dir den Weg weisen.




    -> END
