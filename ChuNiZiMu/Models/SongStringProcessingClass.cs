namespace ChuNiZiMu.Models
{
	public class SongStringProcessingClass
    {

		private static int[] RandomIntArray(int randCount,int length,Random rand)
		{
			int index = 0;
			int[] randomArray = new int[randCount];
			while (index < randCount)
			{
				int randInt = rand.Next(0, length);
				if (randomArray.All(x => x != randInt))
				{
					randomArray[index] = randInt;
					index++;
				}
			}
			return randomArray;
		}

		///<summary>
		/// 处理字符串的类，用于给猜测中曲名增加干扰。
		///</summary>
		public static char[] AddEffect(char[] SongName,Array EffectList, int RandomSeed)
		{
			int songLength = SongName.Length;
			char[] affectedSongName = new char[songLength];
            SongName.CopyTo(affectedSongName,0);
            int[] randomArray = new int[songLength / 4];
			foreach (EffectType effect in EffectList)
			{
				if (effect == EffectType.None)
				{
				
				}
				else if (effect == EffectType.EncryptedFixed)
				{
					Random rand = new Random(RandomSeed);
					randomArray = RandomIntArray(songLength / 4, songLength, rand);
					foreach (int i in randomArray)
					{
						affectedSongName[i] = '▓';
					}
				}
				else if (effect == EffectType.EncryptedRandom)
				{
					Random newRand = new Random();
					randomArray = RandomIntArray(songLength / 4, songLength, newRand);
					foreach (int i in randomArray)
					{
						affectedSongName[i] = '▓';
					}
				}
				else if (effect == EffectType.Reversed)
				{
					Array.Reverse(affectedSongName);
				}
				else if (effect == EffectType.Lacked)
				{
					Random rand = new Random(RandomSeed + 128); // 在保证种子固定的情况下防止与其他效果应用于完全相同的字符上
                    randomArray = RandomIntArray(songLength / 4, songLength, rand);
					Array.Sort(randomArray);
					int lackNum = 0;
					String affectedSongString = new String(affectedSongName);
                    foreach (int i in randomArray)
                    {
						affectedSongString = affectedSongString.Remove(i - lackNum,1);
						lackNum++;
                    }
					affectedSongName = affectedSongString.ToCharArray();
                }
				//todo
				else if (effect == EffectType.Transposed)
				{
					Random rand = new Random(RandomSeed + 256);
                    randomArray = RandomIntArray((int)(songLength / 4) * 2, songLength, rand);
					int transNum = 0;
					while(transNum < randomArray.Length)
					{
						char tempChar = affectedSongName[(int)randomArray[transNum]];
						affectedSongName[(int)randomArray[transNum]] = affectedSongName[(int)randomArray[transNum + 1]];
                        affectedSongName[(int)randomArray[transNum + 1]] = tempChar;
						transNum = transNum + 2;
                    }
                }
                // Invisible 效果不在这里处理
                else
                {
					throw new Exception();
				}
			}
			return affectedSongName;
		}
	}
}
