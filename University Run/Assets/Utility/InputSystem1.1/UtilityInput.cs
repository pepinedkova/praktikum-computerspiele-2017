using UnityEngine;
using UnityEngine.UI;

namespace mech.input
{
    public class UtilityInput
    {
        // Enable read/write on texture + (Meshtype full rect) + use stretch pivot type + pivot is in middle
        public static bool IsPointOverAlpha(Image image, Vector2 posInput)
        {
            RectTransform rectTrans = image.GetComponent<RectTransform>();
            Rect rect = rectTrans.rect;

            Vector3 posInputV3 = new Vector3(posInput.x, posInput.y, 0);
            Vector2 posOnRect = rectTrans.InverseTransformPoint(posInput);

            if(rect.Contains(posOnRect))
            {         
                float ratioInRectX = (posOnRect.x + 0.5f * rect.width) / rect.width;
                float ratioInRectY = (posOnRect.y + 0.5f * rect.height) / rect.height;

                float widthTexture = image.sprite.texture.width;
                float heightTexture = image.sprite.texture.height;

                int xTexture = (int)(widthTexture * ratioInRectX);
                int yTexture = (int)(heightTexture * ratioInRectY);

                Color color;
                try
                {
                    color = image.sprite.texture.GetPixel(xTexture, yTexture);
                }
                catch
                {
                    // not free to read
                    return true;
                }

                return color.a > 0;
            }
            return false;
        }

        public static bool IsPointOverAlphas(Image[] images, Vector2 posInput)
        {
            bool isOverAlpha = false;
            foreach(Image image in images)
            {
                if (IsPointOverAlpha(image, posInput)) isOverAlpha = true;
            }
            return isOverAlpha;
        }
    }
}


    //public static bool IsTouchOnAlpha(Image image, Vector2 posTouch, Camera camMain)
    //{
    //    RectTransform rectImage = image.GetComponent<RectTransform>();

    //    Vector2 posHit;
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(rectImage, posTouch, camMain, out posHit);
    //    Vector2 normPoint = (posHit - rectImage.rect.min);
    //    normPoint.x /= rectImage.rect.width;
    //    normPoint.y /= rectImage.rect.height;

    //    Texture2D texture = image.sprite.texture;

    //    Color color = texture.GetPixel((int)(normPoint.x * texture.width), (int)(normPoint.y * texture.height));
    //    return color.a > 0f;
    //}

    //public static bool IsTouchOnAlpha(Image img, PointerEventData ped)
    //{
    //    RectTransform rectImage = img.GetComponent<RectTransform>();

    //    Vector2 rectPoint;
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(rectImage, ped.position, ped.pressEventCamera, out rectPoint);
    //    Vector2 normPoint = (rectPoint - rectImage.rect.min);
    //    normPoint.x /= rectImage.rect.width;
    //    normPoint.y /= rectImage.rect.height;

    //    Texture2D texture = img.sprite.texture;
    //    Color color = texture.GetPixel((int)(normPoint.x * texture.width), (int)(normPoint.y * texture.height));

    //    return color.a > 0f;
    //}

    //public static bool IsPointOverAlpha(Vector2 posScreen, Image image)
    //{       
    //    RectTransform rectTransform = image.GetComponent<RectTransform>();
    //    Texture2D texture = image.sprite.texture;

    //    Vector2 wanted = rectTransform.InverseTransformPoint(new Vector3(posScreen.x, posScreen.y, 0));
    //    wanted = (wanted - rectTransform.rect.min);

    //    float ratioRectX = wanted.x / rectTransform.rect.width;          
    //    float ratioRectY = wanted.y / rectTransform.rect.height;

    //    int xPixel = (int)(ratioRectX * texture.width);
    //    int yPixel = (int)(ratioRectY * texture.height);

    //    Color color;
    //    try
    //    {
    //        color = texture.GetPixel(xPixel, yPixel);
    //        return color.a > 0;
    //    }
    //    catch
    //    {
    //        DebugUI.Print("Texture Error - Make sure Read/Write is enabled");
    //        return false;
    //    }  
    //}
