-> main

=== main ===
I am the mysterious Dr. XXXXX!
Are you ready to take my test?


    *[Yes!]
        Nobody is ready for my exam!
    -> QuestionCheat
    
    *[No.]
        That's the right attitude!
    -> QuestionCheat
    
    
=== QuestionCheat ===
My exam is different from the ones you're used to.
    *[But why?]
        Listen, I'll explain it to you.
        -> explanation
        
    * [I will still pass!]
        Are you sure about that?
        -> explanation
        
    * [I'm confused.]
        I am too!
        ->QuestionCheat
        

=== explanation ===
The rules of my exam are in a constant state of flux.
*[What does that mean?]
    That they can easily change during the exam.
    ->lastChoice
    
    === lastChoice ===
    You have no chance to circumvent my rules!
    
        *[I'll try anyway!]
            You won't succeed!
            -> END
            
        *[I will pass fairly!]
            Wrong answer!
            ->explanation
            
        *[I don't understand that.]
            Neither do I.
            ->explanation
            
    
        
        


