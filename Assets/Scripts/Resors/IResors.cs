using System;

public interface IResors
{
   // event Action<string> ChangeResors;

    int CountResorses { get; set; }

    void PuckUpResors(int countResors = 1);

    void PuckDownResors(int countResors = 1);
}