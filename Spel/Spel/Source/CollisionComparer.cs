using GameEngine.Source.RandomStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel.Source
{
        class CollisionComparer : IEqualityComparer<Collision>
        {

            public bool Equals(Collision x, Collision y)
            {
                if (x.entity1 == y.entity1 && x.entity2 == y.entity2)
                {
                    return true;
                }
                else if (x.entity1 == y.entity2 && x.entity2 == y.entity1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public int GetHashCode(Collision obj)
            {
                return obj.entity1.GetHashCode() ^ obj.entity2.GetHashCode();
            }
        }
    }
