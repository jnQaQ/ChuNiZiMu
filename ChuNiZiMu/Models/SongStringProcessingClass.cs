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
				if (effect == EffectType.EncryptedFixed)
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
					int lackNum = 0;
                    foreach (int i in randomArray)
                    {
						affectedSongName = affectedSongName.ToString().Remove(i - lackNum,1).ToCharArray();
						lackNum++;
                    }
                }
				//todo
				else if (effect == EffectType.Transposed)
				{
					Random rand = new Random(RandomSeed + 256);
				}
				//todo
				else if (effect == EffectType.Forward)
				{
					Random rand = new Random(RandomSeed + 512);
					int offset = rand.Next(1,4);
					int index;
					for(index = 0;index<songLength;index++)
					{
						char affectedSongChar = affectedSongName[index];
						if(!char.IsAscii(affectedSongChar))
						{
							affectedSongName[index] = affectedSongChar;
						}
						
					}
				}
				//todo
				else if (effect == EffectType.Backward)
				{
					Random rand = new Random(RandomSeed + 1024);
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
