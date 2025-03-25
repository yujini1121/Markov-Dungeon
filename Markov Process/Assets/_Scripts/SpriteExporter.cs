using System.IO;
using UnityEngine;

public class SpriteExporter : MonoBehaviour
{
    public Texture2D spriteSheet; // ���� �ؽ�ó
    public Sprite[] sprites; // �ڵ� Slice�� ��������Ʈ��

    void Start()
    {
        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("��������Ʈ �迭�� ��� �ֽ��ϴ�! Unity���� Sprite �迭�� �����ϼ���.");
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

        Debug.Log("��� ��������Ʈ�� ����Ǿ����ϴ�! ����: " + folderPath);
    }

    void SaveSpriteAsPNG(Sprite sprite, string folderPath)
    {
        // ��������Ʈ ũ�⸸ŭ Texture2D ����
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.rect.x,
            (int)sprite.rect.y,
            (int)sprite.rect.width,
            (int)sprite.rect.height
        );
        texture.SetPixels(pixels);
        texture.Apply();

        // PNG ��ȯ �� ����
        byte[] bytes = texture.EncodeToPNG();
        string filePath = Path.Combine(folderPath, sprite.name + ".png");
        File.WriteAllBytes(filePath, bytes);
        Debug.Log($"��������Ʈ {sprite.name} ���� �Ϸ�: {filePath}");
    }
}
