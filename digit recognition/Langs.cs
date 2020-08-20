using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digit_recognition
{
    public static class Langs
    {
        public static string lang = "Russian";
        public static string load = "Загрузка весов из файла...";
        public static string loadSuccessful = "Загрузка прошла успешно!";
        public static string startSuccessful = "Программа запущена успешно. Для начала выберите графический файл с рукописной цифрой на белом фоне...";
        public static string startWork = "Начало работы...";
        public static string repeat = "Повторите попытку!";
        public static string prepareImageStart = "Начинается подготовка изображения...";
        public static string cropImage = "Обрезка изображения...";
        public static string compressionImage = "Сжатие изображения...";
        public static string updateImage = "Обновление в блоке изображения слева...";
        public static string scanImage = "Сканирование изображения...";
        public static string scanEnded = "Сканирование завершено!";
        public static string foundDigit = "Опознана цифра";
        public static string errorRecogSaved = "Ошибочное распознавание сохранено:";
        public static string errorRecogSaveInfo = "Оригинальное изображение было сохранено в папке ErrorsImages, находящейся рядом с запускаемым файлом программы. Его можно использовать для более точного обучения сети, чтобы таких ошибок более не возникало!";
        public static string saved = "Сохранено";

        public static string titleForm1 = "Распознавание цифр";
        public static string letsGo = "Начать работу";
        public static string recogDigit = "Распознать цифру";
        public static string settingSelection = "Настроить выделение";
        public static string selectionText = "Передвиньте ползунок так, чтобы большая\nчасть цифры стала красной и не было\nвыделено лишнего";
        public static string goContinue = "Готово, продолжить";
        public static string error = "Неверно!";
        public static string textOutput = "Консоль";

        public static string titleFormChoice = "Выберите способ ввода изображения";
        public static string imageFromComputer = "Выбрать изображение с компьютера";
        public static string drawImage = "Нарисовать цифру самому";
        public static string userDoChoice = "Пользователь выбирает изображение...";
        public static string imageNotChoice = "Изображение не выбрано! Ожидание нового выбора...";
        public static string imageNotChange = "Выбранное изображение не было изменено!";
        public static string niceChoice = "Пользователь выбрал изображение с рукописной цифрой. Ожидание нажатия кнопки распознавания...";


        public static string titleFormDraw = "Нарисуйте цифур";
        public static string recog = "Распознать";
        public static string clean = "Очистить";
        public static string goodDraw = "Изображение нарисовано и готово к распознаванию!";

        public static void updateLang(string lg)
        {
            if (lg.Equals("Russian"))
            {
                lang = "Russian";
                load = "Загрузка весов из файла...";
                loadSuccessful = "Загрузка прошла успешно!";
                startSuccessful = "Программа запущена успешно. Для начала выберите графический файл с рукописной цифрой на белом фоне...";
                startWork = "Начало работы...";
                repeat = "Повторите попытку!";
                prepareImageStart = "Начинается подготовка изображения...";
                cropImage = "Обрезка изображения...";
                compressionImage = "Сжатие изображения...";
                updateImage = "Обновление в блоке изображения слева...";
                scanImage = "Сканирование изображения...";
                scanEnded = "Сканирование завершено!";
                foundDigit = "Опознана цифра";
                errorRecogSaved = "Ошибочное распознавание сохранено:";
                errorRecogSaveInfo = "Оригинальное изображение было сохранено в папке ErrorsImages, находящейся рядом с запускаемым файлом программы. Его можно использовать для более точного обучения сети, чтобы таких ошибок более не возникало!";
                saved = "Сохранено";

                titleForm1 = "Распознавание цифр";
                letsGo = "Начать работу";
                recogDigit = "Распознать цифру";
                settingSelection = "Настроить выделение";
                selectionText = "Передвиньте ползунок так, чтобы большая\nчасть цифры стала красной и не было\nвыделено лишнего";
                goContinue = "Готово, продолжить";
                error = "Неверно!";
                textOutput = "Консоль";

                titleFormChoice = "Выберите способ ввода изображения";
                imageFromComputer = "Выбрать изображение с компьютера";
                drawImage = "Нарисовать цифру самому";
                userDoChoice = "Пользователь выбирает изображение...";
                imageNotChoice = "Изображение не выбрано! Ожидание нового выбора...";
                imageNotChange = "Выбранное изображение не было изменено!";
                niceChoice = "Пользователь выбрал изображение с рукописной цифрой. Ожидание нажатия кнопки распознавания...";

                titleFormDraw = "Нарисуйте цифур";
                recog = "Распознать";
                clean = "Очистить";
                goodDraw = "Изображение нарисовано и готово к распознаванию!";
                return;
            }
            if (lg.Equals("English"))
            {
                lang = "English";
                load = "Loading weights from a file ...";
                loadSuccessful = "Load was successful!";
                startSuccessful = "The program started successfully. To get started, select a graphic file with a handwritten number on a white background ...";
                startWork = "Beginning of work...";
                repeat = "Try again!";
                prepareImageStart = "Image preparation begins ...";
                cropImage = "Crop Image ...";
                compressionImage = "Image Compression ...";
                updateImage = "Update in the image block on the left ...";
                scanImage = "Scan Image ...";
                scanEnded = "Scan completed!";
                foundDigit = "Digit recognized";
                errorRecogSaved = "Erroneous recognition saved:";
                errorRecogSaveInfo = "The original image was saved in the ErrorsImages folder located next to the program file being launched. It can be used to more accurately train the network so that such errors no longer occur!";
                saved = "Saved";

                titleForm1 = "Digit recognition";
                letsGo = "Get started";
                recogDigit = "Recognize digit";
                settingSelection = "Set selection";
                selectionText = "Move the slider so that most of the number turns\nred and there is no excess";
                goContinue = "Done, continue";
                error = "Wrong!";
                textOutput = "Console";


                titleFormChoice = "Choose image input method";
                imageFromComputer = "Select computer image";
                drawImage = "Draw the number yourself";
                userDoChoice = "User selects image ...";
                imageNotChoice = "No image selected! Waiting for a new choice ...";
                imageNotChange = "The selected image has not been changed!";
                niceChoice = "The user selected a handwritten image. Waiting for the recognition button to click ...";


                titleFormDraw = "Draw numbers";
                recog = "To recognize";
                clean = "Clear";
                goodDraw = "The image is drawn and ready for recognition!";
                return;
            }
        }
    }
}
