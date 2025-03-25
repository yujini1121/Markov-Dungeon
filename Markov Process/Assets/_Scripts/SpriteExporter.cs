using System.IO;
using UnityEngine;

public class SpriteExporter : MonoBehaviour
{
    public Texture2D spriteSheet; // 원본 텍스처
    public Sprite[] sprites; // 자동 Slice된 스프라이트들

    void Start()
    {
        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("스프라이트 배열이 비어 있습니다! Unity에서 Sprite 배열을 설정하세요.");
            return;
        }

        string folderPath = Application.dataPath + "/ExportedSprites";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (Sprite sprite in sprites)
        {
            SaveSpriteAsPNG(sprite, folderPath);
        }

        Debug.Log("모든 스프라이트가 저장되었습니다! 폴더: " + folderPath);
    }

    void SaveSpriteAsPNG(Sprite sprite, string folderPath)
    {
        // 스프라이트 크기만큼 Texture2D 생성
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.rect.x,
            (int)sprite.rect.y,
            (int)sprite.rect.width,
            (int)sprite.rect.height
        );
        texture.SetPixels(pixels);
        texture.Apply();

        // PNG 변환 및 저장
        byte[] bytes = texture.EncodeToPNG();
        string filePath = Path.Combine(folderPath, sprite.name + ".png");
        File.WriteAllBytes(filePath, bytes);
        Debug.Log($"스프라이트 {sprite.name} 저장 완료: {filePath}");
    }
}
