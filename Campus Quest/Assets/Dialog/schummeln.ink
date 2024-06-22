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
    Das sie sich auch einfach während der Prüfung ändern können.
    ->letzteChoice
    
    === letzteChoice ===
    Du hast keine Chance meine Regeln zu umgehen!
    
        *[Ich werde es trotzdem versuchen!]
            Es wird dir nicht gelingen!
            -> END
            
        *[Ich werde fair bestehen!]
            Falsche Antwort!
            ->erklaerung
            
        *[Das verstehe ich nicht.]
            Ich auch nicht.
            ->erklaerung
            
    
        
        

