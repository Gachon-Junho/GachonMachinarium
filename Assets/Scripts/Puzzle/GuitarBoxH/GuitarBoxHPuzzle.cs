using System;
using System.Linq;
using UnityEngine;

public class GuitarBoxHPuzzle : Puzzle
{
    [SerializeField] private ClickPoint[] clickPoints;

    [SerializeField] private GameObject userControl;

    private ClickInfo[] info;

    public override void Initialize()
    {
        info = new ClickInfo[clickPoints.Length];

        for (int i = 0; i < info.Length; i++)
        {
            var clickPoint = clickPoints[i];

            // TODO: 랜덤 색상, 회전값이 필요할 수 있음.
            info[i] = new ClickInfo(clickPoint);
            clickPoint.CollisionEnter += (_, _) =>
            {
                var click = info.FirstOrDefault(i => i.Equals(clickPoint));

                if (click == null)
                    return;

                click.Collided = true;
            };

            clickPoint.CollisionExit += (_, _) =>
            {
                var click = info.FirstOrDefault(i => i.Equals(clickPoint));

                if (click == null)
                    return;

                click.Collided = false;
            };
        }
    }

    protected override bool CheckCondition()
    {
        return info.ToList().TrueForAll(i => i.Clicked);
    }

    private void OnMouseDown()
    {
        CommitChange();
    }

    protected override void CommitChange()
    {
        var collided = info.FirstOrDefault(i => i.Collided);

        if (collided == null)
            return;

        collided.Clicked = true;

        base.CommitChange();
    }

    protected override void OnFailed()
    {
        base.OnFailed();

        // TODO: 실패시 초기회?
    }

    private class ClickInfo : IEquatable<ClickPoint>
    {
        public bool Collided;
        public bool Clicked;

        private ClickPoint point;

        public ClickInfo(ClickPoint point)
        {
            this.point = point;
        }

        public bool Equals(ClickPoint other)
        {
            return ReferenceEquals(point, other);
        }
    }
}
