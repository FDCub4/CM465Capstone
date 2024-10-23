using Manufaktura.Controls.Model;
using Manufaktura.Music.Model.MajorAndMinor;
using Manufaktura.Music.Model;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.IO;
using System.Windows.Forms;
using MathNet.Numerics.Distributions;
using static System.Formats.Asn1.AsnWriter;
using System.Reactive;
using System.Windows.Navigation;

public class TestData : ViewModel
{
    private Score data;
    public Score Data
    {
        get { return data; }
        set { data = value; OnPropertyChanged(() => Data); }
    }

    readonly double[] AudioValues;
    readonly WasapiCapture AudioDevice;
    readonly double[] FftValues;

    public void LoadTestData()
    {
        // Create a new score
        var score = new Score();

        // Define constants and variables
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "frequencies.txt");
        const int maxNotesPerStaff = 15;  // Max notes before creating a new staff
        const double minPianoFrequency = 27.5;  // A0
        const double maxPianoFrequency = 4186.0; // C8
        int totalNotes = 0;
        double lastTimestamp = 0.0;

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;

                // Create the first staff
                var currentStaff = new Staff();
                currentStaff.Elements.Add(Clef.Treble);
                currentStaff.Elements.Add(new Manufaktura.Controls.Model.Key(0));
                score.Staves.Add(currentStaff);

                while ((line = sr.ReadLine()) != null && totalNotes < 40)  // Limit total notes
                {
                    var parts = line.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length >= 8 &&
                        parts[0].Trim() == "Timestamp:" &&
                        double.TryParse(parts[1], out double timestamp) &&  // Extract timestamp
                        parts[2].Trim() == "sec" &&
                        parts[3].Trim() == "Peak" &&
                        parts[4].Trim() == "Frequency:" &&
                        double.TryParse(parts[5].Replace(",", "").Trim(), out double peakFrequency)) // Extract frequency
                    {
                        //if (peakFrequency < minPianoFrequency || peakFrequency > maxPianoFrequency)
                        //    continue; // Skip out-of-range frequencies

                        double timeDifference = timestamp - lastTimestamp;
                        lastTimestamp = timestamp;
                        if (timeDifference > 2.0)
                            timeDifference = 2.0;

                        // Get the note, octave, and accidental alteration
                        
                        // Create and add note to the staff
                        RhythmicDuration rhythmicDuration = ConvertToRhythmicDuration(timeDifference);
                        currentStaff.Elements.Add(new Note(FrequencyDetermined(peakFrequency), rhythmicDuration));
                        totalNotes++;

                        // Add barline after every 4 notes
                        if (totalNotes % 4 == 0)
                            currentStaff.AddBarline();

                        // If max notes per staff are reached, create a new staff
                        //if (totalNotes % maxNotesPerStaff == 0)
                        //{
                        //    currentStaff = new Staff();
                        //    currentStaff.Elements.Add(Clef.Treble);
                        //    currentStaff.Elements.Add(new Manufaktura.Controls.Model.Key(0));
                        //    score.Staves.Add(currentStaff); // Add new staff to the score
                        //}
                        //changes worked
                    }
                }

                Data = score;  // Update score with staves
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }
    }

    public static Pitch FrequencyDetermined(double frequency)
    {
        if (frequency < 32.703) return Pitch.C1; // C1
        if (frequency >= 32.703 && frequency < 34.648) return Pitch.CSharp1; // C#1 / Db1
        if (frequency >= 34.648 && frequency < 36.708) return Pitch.D1; // D1
        if (frequency >= 36.708 && frequency < 38.891) return Pitch.DSharp1; // D#1 / Eb1
        if (frequency >= 38.891 && frequency < 41.203) return Pitch.E1; // E1
        if (frequency >= 41.203 && frequency < 43.654) return Pitch.F1; // F1
        if (frequency >= 43.654 && frequency < 46.249) return Pitch.FSharp1; // F#1 / Gb1
        if (frequency >= 46.249 && frequency < 48.999) return Pitch.G1; // G1
        if (frequency >= 48.999 && frequency < 51.913) return Pitch.GSharp1; // G#1 / Ab1
        if (frequency >= 51.913 && frequency < 55.0) return Pitch.A1; // A1
        if (frequency >= 55.0 && frequency < 58.27) return Pitch.ASharp1; // A#1 / Bb1
        if (frequency >= 58.27 && frequency < 61.735) return Pitch.B1; // B1

        if (frequency >= 61.735 && frequency < 65.406) return Pitch.C2; // C2
        if (frequency >= 65.406 && frequency < 69.296) return Pitch.CSharp2; // C#2 / Db2
        if (frequency >= 69.296 && frequency < 73.416) return Pitch.D2; // D2
        if (frequency >= 73.416 && frequency < 77.782) return Pitch.DSharp2; // D#2 / Eb2
        if (frequency >= 77.782 && frequency < 82.407) return Pitch.E2; // E2
        if (frequency >= 82.407 && frequency < 87.307) return Pitch.F2; // F2
        if (frequency >= 87.307 && frequency < 92.499) return Pitch.FSharp2; // F#2 / Gb2
        if (frequency >= 92.499 && frequency < 97.999) return Pitch.G2; // G2
        if (frequency >= 97.999 && frequency < 103.826) return Pitch.GSharp2; // G#2 / Ab2
        if (frequency >= 103.826 && frequency < 110.0) return Pitch.A2; // A2
        if (frequency >= 110.0 && frequency < 116.541) return Pitch.ASharp2; // A#2 / Bb2
        if (frequency >= 116.541 && frequency < 123.471) return Pitch.B2; // B2

        if (frequency >= 123.471 && frequency < 130.813) return Pitch.C3; // C3
        if (frequency >= 130.813 && frequency < 138.591) return Pitch.CSharp3; // C#3 / Db3
        if (frequency >= 138.591 && frequency < 146.832) return Pitch.D3; // D3
        if (frequency >= 146.832 && frequency < 155.563) return Pitch.DSharp3; // D#3 / Eb3
        if (frequency >= 155.563 && frequency < 164.814) return Pitch.E3; // E3
        if (frequency >= 164.814 && frequency < 174.614) return Pitch.F3; // F3
        if (frequency >= 174.614 && frequency < 184.997) return Pitch.FSharp3; // F#3 / Gb3
        if (frequency >= 184.997 && frequency < 195.998) return Pitch.G3; // G3
        if (frequency >= 195.998 && frequency < 207.652) return Pitch.GSharp3; // G#3 / Ab3
        if (frequency >= 207.652 && frequency < 220.0) return Pitch.A3; // A3
        if (frequency >= 220.0 && frequency < 233.082) return Pitch.ASharp3; // A#3 / Bb3
        if (frequency >= 233.082 && frequency < 246.942) return Pitch.B3; // B3

        if (frequency >= 246.942 && frequency < 261.626) return Pitch.C4; // C4 (Middle C)
        if (frequency >= 261.626 && frequency < 277.183) return Pitch.CSharp4; // C#4 / Db4
        if (frequency >= 277.183 && frequency < 293.665) return Pitch.D4; // D4
        if (frequency >= 293.665 && frequency < 311.127) return Pitch.DSharp4; // D#4 / Eb4
        if (frequency >= 311.127 && frequency < 329.628) return Pitch.E4; // E4
        if (frequency >= 329.628 && frequency < 349.228) return Pitch.F4; // F4
        if (frequency >= 349.228 && frequency < 369.994) return Pitch.FSharp4; // F#4 / Gb4
        if (frequency >= 369.994 && frequency < 391.995) return Pitch.G4; // G4
        if (frequency >= 391.995 && frequency < 415.305) return Pitch.GSharp4; // G#4 / Ab4
        if (frequency >= 415.305 && frequency < 440.0) return Pitch.A4; // A4
        if (frequency >= 440.0 && frequency < 466.164) return Pitch.ASharp4; // A#4 / Bb4
        if (frequency >= 466.164 && frequency < 493.883) return Pitch.B4; // B4

        if (frequency >= 493.883 && frequency < 523.251) return Pitch.C5; // C5
        if (frequency >= 523.251 && frequency < 554.365) return Pitch.CSharp5; // C#5 / Db5
        if (frequency >= 554.365 && frequency < 587.33) return Pitch.D5; // D5
        if (frequency >= 587.33 && frequency < 622.254) return Pitch.DSharp5; // D#5 / Eb5
        if (frequency >= 622.254 && frequency < 659.255) return Pitch.E5; // E5
        if (frequency >= 659.255 && frequency < 698.456) return Pitch.F5; // F5
        if (frequency >= 698.456 && frequency < 739.989) return Pitch.FSharp5; // F#5 / Gb5
        if (frequency >= 739.989 && frequency < 783.991) return Pitch.G5; // G5
        if (frequency >= 783.991 && frequency < 830.609) return Pitch.GSharp5; // G#5 / Ab5
        if (frequency >= 830.609 && frequency < 880.0) return Pitch.A5; // A5
        if (frequency >= 880.0 && frequency < 932.328) return Pitch.ASharp5; // A#5 / Bb5
        if (frequency >= 932.328 && frequency < 987.767) return Pitch.B5; // B5

        if (frequency >= 987.767 && frequency < 1046.502) return Pitch.C6; // C6
        if (frequency >= 1046.502 && frequency < 1108.731) return Pitch.CSharp6; // C#6 / Db6
        if (frequency >= 1108.731 && frequency < 1174.659) return Pitch.D6; // D6
        if (frequency >= 1174.659 && frequency < 1244.508) return Pitch.DSharp6; // D#6 / Eb6
        if (frequency >= 1244.508 && frequency < 1318.51) return Pitch.E6; // E6
        if (frequency >= 1318.51 && frequency < 1396.913) return Pitch.F6; // F6
        if (frequency >= 1396.913 && frequency < 1479.978) return Pitch.FSharp6; // F#6 / Gb6
        if (frequency >= 1479.978 && frequency < 1567.982) return Pitch.G6; // G6
        if (frequency >= 1567.982 && frequency < 1661.219) return Pitch.GSharp6; // G#6 / Ab6
        if (frequency >= 1661.219 && frequency < 1760.0) return Pitch.A6; // A6
        if (frequency >= 1760.0 && frequency < 1864.655) return Pitch.ASharp6; // A#6 / Bb6
        if (frequency >= 1864.655 && frequency < 1975.533) return Pitch.B6; // B6

        if (frequency >= 1975.533 && frequency < 2093.005) return Pitch.C7; // C7
        if (frequency >= 2093.005 && frequency < 2217.461) return Pitch.CSharp7; // C#7 / Db7
       

        // If the frequency is outside of the defined ranges, return a default or handle the error.
        return Pitch.Cb3; // You might want to define an Unknown pitch.
    }





    //var score = Score.CreateOneStaffScore(Clef.Treble, new MajorScale(Step.C, false));
    //score.FirstStaff.Elements.Add(new Note(Pitch.ASharp4, RhythmicDuration.Quarter));
    //score.FirstStaff.Elements.Add(new Note(Pitch.B4, RhythmicDuration.Quarter));
    //score.FirstStaff.Elements.Add(new Note(Pitch.C5, RhythmicDuration.Half));
    //score.FirstStaff.Elements.Add(new Barline());
    //Data = score;

    //var secondStaff = new Staff();
    //secondStaff.Elements.Add(Clef.Treble);
    //secondStaff.Elements.Add(new Manufaktura.Controls.Model.Key(0));
    //secondStaff.Elements.Add(new Note(Pitch.G4, RhythmicDuration.Whole));
    //secondStaff.Elements.Add(new Barline());
    //score.Staves.Add(secondStaff);
    //score.Staves.Add(new Staff());

    //score.ThirdStaff.Elements.Add(Clef.Tenor);
    //score.ThirdStaff.Elements.Add(new Manufaktura.Controls.Model.Key(0));
    //score.ThirdStaff.Elements.Add(new Note(Pitch.D4, RhythmicDuration.Half));
    //score.ThirdStaff.Elements.Add(new Note(Pitch.E4, RhythmicDuration.Half));
    //score.ThirdStaff.Elements.Add(new Barline());
    //score.Staves.Add(new Staff());

    //score.Staves[3].Elements.Add(Clef.Bass);    //0-based index
    //score.Staves[3].Elements.Add(new Manufaktura.Controls.Model.Key(0));
    //score.Staves[3].Elements.Add(new Note(Pitch.G3, RhythmicDuration.Half));
    //score.Staves[3].Elements.Add(new Note(Pitch.C3, RhythmicDuration.Half));
    //score.Staves[3].Elements.Add(new Barline());
    //for(int i =1; i<10; i++)
    //{
    //    var score = Score.CreateOneStaffScore(Clef.Treble, new MajorScale(Step.C, false));
    //    score.FirstStaff.Elements.Add(new Note(Step.i.toString(), RhythmicDuration.Quarter));
    //}
    //might have to recreate a class within our own project to handle pitch, see website:
    //https://bitbucket.org/Ajcek/manufakturalibraries/src/master/Manufaktura.Music/Model/Pitch.cs
    //this shows how they determine pitch, so for us, we will need to recreate the library/dictionary
    //for our own inputs, after data analysis has occured to find frequency/pitch
    //var score = Score.CreateOneStaffScore(Clef.Treble, new MajorScale(Step.C, false));

    //score.FirstStaff.Elements.Add(new Note(Pitch.C1, RhythmicDuration.Quarter));
    //Data = score;
    //score.FirstStaff.Elements.Add(new Barline());

    private static RhythmicDuration ConvertToRhythmicDuration(double seconds)
    {
        var durationMapping = new (RhythmicDuration duration, double value)[]
        {
        (RhythmicDuration.Eighth, 0.25),
        (RhythmicDuration.Quarter, 0.5),
        (RhythmicDuration.Half, 1.0),
        (RhythmicDuration.Whole, 2.0)
        };

        RhythmicDuration closestDuration = durationMapping[0].duration;
        double closestDifference = Math.Abs(seconds - durationMapping[0].value);

        foreach (var (duration, value) in durationMapping)
        {
            double difference = Math.Abs(seconds - value);
            if (difference < closestDifference)
            {
                closestDifference = difference;
                closestDuration = duration;
            }
        }

        return closestDuration;
    }
    
}

