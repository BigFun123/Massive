using BulletSharp;
using Massive;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MPhysicsCharacter : MObject
  {
    PairCachingGhostObject ghostObject;
    KinematicCharacterController character;

    public MPhysicsCharacter() : base(EType.PhysicsCharacter, "PhysicsCharacter")
    {

    }

    public void Setup(Vector3d Pos)
    {
      const float characterHeight = 1.75f;
      const float characterWidth = 1.75f;
      var capsule = new CapsuleShape(characterWidth, characterHeight);
      ghostObject = new PairCachingGhostObject()
      {
        CollisionShape = capsule,
        CollisionFlags = CollisionFlags.CharacterObject,
        WorldTransform = Matrix4d.CreateTranslation(Pos)
      };
      const float stepHeight = 0.35f;
      character = new KinematicCharacterController(ghostObject, capsule, stepHeight);
      MPhysics.Instance.World.AddAction(character);
      MPhysics.Instance.World.AddCollisionObject(ghostObject, CollisionFilterGroups.CharacterFilter, CollisionFilterGroups.StaticFilter | CollisionFilterGroups.DefaultFilter);
    }
  }
}
