namespace ChuNiZiMu.Models;

// 特殊效果类型

public enum EffectType
{
    None = 0,   // 无效果
    EncryptedFixed = 100,  // 部分字母被加密，无法被揭露（被加密的字母固定）
    EncryptedRandom = 101, // 部分字母被加密，无法被揭露（被加密的字母在每次猜测后随机变化）
    Reversed = 200,   // 谜面被左右反转
    Lacked = 300,   //谜面中的部分字母完全消失
    Transposed = 400,   // 谜面中的部分字母被随机调换顺序
    Forward = 500,  // 谜面显示的揭露字母被随机前移1-3个字母
    Backward = 600, // 谜面显示的揭露字母被随机后移1-3个字母
    Invisible = 700,   // 谜面完全不可见，只能看到猜测进度
}