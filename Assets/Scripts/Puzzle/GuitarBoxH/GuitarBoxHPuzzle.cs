using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GuitarBoxHPuzzle : Puzzle, IPointerClickHandler
{
    [SerializeField]
    private ClickPoint[] clickPoints;

    [SerializeField]
    private AdjustableColor[] lights;

    [SerializeField]
    private ClickPoint userControl;

    [SerializeField]
    private float revolutionPerSecond;

    private ClickInfo[] info;
    private int chance;

    public override void Initialize()
    {
        info = new ClickInfo[clickPoints.Length];
        chance = clickPoints.Length;

        userControl.CollisionEnter += (_, other) =>
        {
            var click = info.FirstOrDefault(i => i.PointObject == other.gameObject);

            if (click == null)
                return;

            click.Collided = true;
        };

        userControl.CollisionExit += (_, other) =>
        {
            var click = info.FirstOrDefault(i => i.PointObject == other.gameObject);

            if (click == null)
                return;

            click.Collided = false;
        };

        for (int i = 0; i < info.Length; i++)
        {
            var clickPoint = clickPoints[i];
            info[i] = new ClickInfo(clickPoint);

            clickPoint.RotateTo(new Vector3(0, 0, Random.Range(0f, 360f)));
            lights[i].ColorTo(Color.white);
        }
    }

    private void Update()
    {
        if (State == PuzzlePlayingState.Playing)
            userControl.transform.Rotate(new Vector3(0, 0, revolutionPerSecond * 360 * Time.deltaTime));
    }

    protected override bool CheckCondition()
    {
        // 모두 다 클릭 되어야하고, 기회는 3번
        return info.ToList().TrueForAll(i => i.Clicked) && chance >= 0;
    }

    protected override void CommitChange()
    {
        var collided = info.FirstOrDefault(i => i.Collided);
        int index = Array.IndexOf(clickPoints, collided?.Point);

        chance--;

        // 실패
        if (chance <= 0 && collided == null)
            UpdateState(PuzzlePlayingState.Failed);

        if (collided == null || index == -1)
            return;

        collided.Clicked = true;
        lights[index].ColorTo(collided.Point.Color, 0.3f, Easing.Out);

        base.CommitChange();
    }

    protected override void OnFailed()
    {
        base.OnFailed();

        lights.ForEach(l => l.ColorTo(Color.black, 0.5f, Easing.OutQuint));
        print("Fail!");

        this.StartDelayedSchedule(() =>
        {
            Initialize();
            UpdateState(PuzzlePlayingState.Playing);

        }, 1);

        // TODO: 실패시 초기회?
    }

    protected override void OnCompleted()
    {
        base.OnCompleted();

        clickPoints.ForEach(p =>
        {
            p.RotateTo(new Vector3(0, 0, Random.Range(-360f, 360f)), 0.7f, Easing.OutQuint);
            p.FadeTo(0, 0.7f, Easing.OutQuint);
        });

        print("Complete!");

        this.StartDelayedSchedule(() =>
        {
            gameObject.SetActive(false);
            PuzzlePanel.SetActive(false);
        }, 1);
        // TODO: 성공시 아이템 지급?

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CommitChange();
    }

    private class ClickInfo : IEquatable<ClickPoint>
    {
        public bool Collided;
        public bool Clicked;

        public GameObject PointObject => Point.gameObject;
        public ClickPoint Point { get; }

        public ClickInfo(ClickPoint point)
        {
            Point = point;
        }

        public bool Equals(ClickPoint other)
        {
            return ReferenceEquals(Point, other);
        }
    }
}
