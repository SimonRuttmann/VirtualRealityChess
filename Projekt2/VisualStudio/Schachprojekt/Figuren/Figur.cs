using System;
using System.Collections.Generic;
using System.Text;

namespace Schachprojekt
{
    public abstract class Figur
    {
        bool IstGeschlagen = false;
        Position Position;
        string Id;

        public Figur( bool gesch, Position pos, string id)
        {
            IstGeschlagen = gesch;
            Position = pos;
            Id = id; 
        }
        public abstract Position[] CheckBewPos();
        public void BewegeNachPos(Position pos)
        {

        }
        public Position GetPosition()
        {
            return null;
        }

        public abstract void Schlage_Anim();
        public abstract void Sterbe_Anim();
        public abstract void Bewege_Anim();
    }

    public class Koenig : Figur
    {
        public Koenig(bool gesch, Position pos, string id) : base(gesch,pos,id)
        { }

        public override void Bewege_Anim()
        {
            throw new NotImplementedException();
        }

        public override Position[] CheckBewPos()
        {
            throw new NotImplementedException();
        }

        public override void Schlage_Anim()
        {
            throw new NotImplementedException();
        }

        public override void Sterbe_Anim()
        {
            throw new NotImplementedException();
        }
    }

    public class Dame : Figur
    {
        public Dame(bool gesch, Position pos, string id) : base(gesch, pos, id)
        { }

        public override void Bewege_Anim()
        {
            throw new NotImplementedException();
        }

        public override Position[] CheckBewPos()
        {
            throw new NotImplementedException();
        }

        public override void Schlage_Anim()
        {
            throw new NotImplementedException();
        }

        public override void Sterbe_Anim()
        {
            throw new NotImplementedException();
        }
    }

    public class Laeufer : Figur
    {
        public Laeufer(bool gesch, Position pos, string id) : base(gesch, pos, id)
        { }

        public override void Bewege_Anim()
        {
            throw new NotImplementedException();
        }

        public override Position[] CheckBewPos()
        {
            throw new NotImplementedException();
        }

        public override void Schlage_Anim()
        {
            throw new NotImplementedException();
        }

        public override void Sterbe_Anim()
        {
            throw new NotImplementedException();
        }
    }

    public class Turm : Figur
    {
        public Turm(bool gesch, Position pos, string id) : base(gesch, pos, id)
        { }

    public override void Bewege_Anim()
        {
            throw new NotImplementedException();
        }

        public override Position[] CheckBewPos()
        {
            throw new NotImplementedException();
        }

        public override void Schlage_Anim()
        {
            throw new NotImplementedException();
        }

        public override void Sterbe_Anim()
        {
            throw new NotImplementedException();
        }
    }

    public class Springer : Figur
    {
        public Springer(bool gesch, Position pos, string id) : base(gesch, pos, id)
        { }

        public override void Bewege_Anim()
        {
            throw new NotImplementedException();
        }

        public override Position[] CheckBewPos()
        {
            throw new NotImplementedException();
        }

        public override void Schlage_Anim()
        {
            throw new NotImplementedException();
        }

        public override void Sterbe_Anim()
        {
            throw new NotImplementedException();
        }
    }

    public class Bauer : Figur
    {
        public Bauer(bool gesch, Position pos, string id) : base(gesch, pos, id)
        { }

        public override void Bewege_Anim()
        {
            throw new NotImplementedException();
        }

        public override Position[] CheckBewPos()
        {
            throw new NotImplementedException();
        }

        public override void Schlage_Anim()
        {
            throw new NotImplementedException();
        }

        public override void Sterbe_Anim()
        {
            throw new NotImplementedException();
        }
    }
}
