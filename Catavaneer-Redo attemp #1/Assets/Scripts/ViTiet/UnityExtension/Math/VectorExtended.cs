using UnityEngine;

namespace ViTiet.UnityExtension.Math
{
    public static class VectorExtended
    {
        /// <summary>
        /// Returns a point on the selected plane related to the center
        /// </summary>
        public static Vector3 GetPoint(Vector3 center, float pointA, float pointB, Plane plane)
        {
            switch (plane)
            {
                case Plane.XY:
                    return new Vector3(center.x + pointA, center.y + pointB, center.z);
                case Plane.XZ:
                    return new Vector3(center.x + pointA, center.y, center.z + pointB);
                case Plane.YZ:
                    return new Vector3(center.x, center.y + pointA, center.z + pointB);
            }

            return new Vector3(center.x, center.y, center.z);
        }

        /// <summary>
        /// Returns directional vector from 2 points
        /// </summary>
        public static Vector3 GetDirectionVector(Vector3 originPoint, Vector3 destinationPoint)
        {
            Vector3 directionVector = new Vector3(destinationPoint.x - originPoint.x, destinationPoint.y - originPoint.y, destinationPoint.z - originPoint.z);
            return directionVector;
        }

        /// <summary>
        /// Returns destination point
        /// </summary>
        public static Vector3 GetDestinationPoint(Vector3 directionVector, Vector3 originPoint)
        {
            Vector3 destinationPoint = new Vector3(directionVector.x + originPoint.x, directionVector.y + originPoint.y, directionVector.z + originPoint.z);
            return destinationPoint;
        }

        /// <summary>
        /// Returns origin point
        /// </summary>
        public static Vector3 GetOriginPoint(Vector3 directionVector, Vector3 destinationPoint)
        {
            Vector3 originPoint = new Vector3(destinationPoint.x - directionVector.x, destinationPoint.y - directionVector.y, destinationPoint.z - directionVector.z);
            return originPoint;
        }

        /// <summary>
        /// Returns new points that rotates around x axis of center
        /// </summary>
        public static Vector3[] GetRotatePointsXAxis(Transform center, Vector3[] points)
        {
            Vector3[] rotatePoints = points;
            Vector3 directionVector = new Vector3();

            // Rotate x axis
            for (int i = 0; i < rotatePoints.Length; i++)
            {
                directionVector = GetDirectionVector(center.position, rotatePoints[i]);
                rotatePoints[i].x = directionVector.x + center.position.x;
                rotatePoints[i].y = directionVector.y * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.x) - directionVector.z * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.x) + center.position.y;
                rotatePoints[i].z = directionVector.y * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.x) + directionVector.z * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.x) + center.position.z;
            }

            return rotatePoints;
        }

        /// <summary>
        /// Returns new points that rotates around y axis of center
        /// </summary>
        public static Vector3[] GetRotatePointsYAxis(Transform center, Vector3[] points)
        {
            Vector3[] rotatePoints = points;
            Vector3 directionVector = new Vector3();

            // Rotate y axis
            for (int i = 0; i < rotatePoints.Length; i++)
            {
                directionVector = GetDirectionVector(center.position, rotatePoints[i]);
                rotatePoints[i].x = directionVector.x * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.y) + directionVector.z * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.y) + center.position.x;
                rotatePoints[i].y = directionVector.y + center.position.y;
                rotatePoints[i].z = -directionVector.x * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.y) + directionVector.z * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.y) + center.position.z;
                //rotatePoints[i].z = -(directionVector.x * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.y) + directionVector.z * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.y)) + center.position.z;
            }

            return rotatePoints;
        }

        /// <summary>
        /// Returns new points that rotates around z axis of center
        /// </summary>
        public static Vector3[] GetRotatePointsZAxis(Transform center, Vector3[] points)
        {
            Vector3[] rotatePoints = points;
            Vector3 directionVector = new Vector3();

            // Rotate z axis
            for (int i = 0; i < rotatePoints.Length; i++)
            {
                directionVector = GetDirectionVector(center.position, rotatePoints[i]);
                rotatePoints[i].x = directionVector.x * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.z) - directionVector.y * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.z) + center.position.x;
                rotatePoints[i].y = directionVector.x * Mathf.Sin(Mathf.PI / 180 * center.eulerAngles.z) + directionVector.y * Mathf.Cos(Mathf.PI / 180 * center.eulerAngles.z) + center.position.y;
                rotatePoints[i].z = directionVector.z + center.position.z;
            }

            return rotatePoints;
        }
    }

    /// <summary>
    /// Plane dimention
    /// </summary>
    public enum Plane
    {
        XY, XZ, YZ
    }
}
