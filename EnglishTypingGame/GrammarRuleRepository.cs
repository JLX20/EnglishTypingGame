using System.Collections.Generic;
using System.Linq;

namespace EnglishTypingGame
{
    public static class GrammarRuleRepository
    {
        private static readonly List<GrammarRule> Rules = new List<GrammarRule>
        {
            // =====================================================
            // NUMBERS
            // =====================================================

            new GrammarRule
            {
                Title = "Большие числа",
                Category = "Числа",
                Level = "A1",
                ShortExplanation = "Большие числа в английском строятся из сотен, десятков и единиц.",
                WhenToUse = "Используй hundred для сотен, thousand для тысячи. После конкретного числа hundred обычно не получает окончание -s.",
                Formula = "100 — one hundred\n200 — two hundred\n900 — nine hundred\n1000 — one thousand",
                Examples = "100 — one hundred\n200 — two hundred\n500 — five hundred\n900 — nine hundred\n1000 — one thousand",
                CommonMistakes = "Неправильно: two hundreds\nПравильно: two hundred\n\nНеправильно: five hundreds\nПравильно: five hundred",
                PracticeQuestion = "Напиши 200 словами.",
                PracticeAnswer = "two hundred",
                PracticeExplanation = "200 = two hundred. Не two hundreds."
            },

            // =====================================================
            // TENSE TYPES: SIMPLE / CONTINUOUS / PERFECT
            // =====================================================

            new GrammarRule
            {
                Title = "Simple / Continuous / Perfect",
                Category = "Времена",
                Level = "A1-A2",
                ShortExplanation = "Это три основные формы времени. Simple говорит о факте или обычном действии. Continuous говорит о процессе. Perfect говорит о результате или опыте.",
                WhenToUse = "Simple — действие вообще, регулярно или факт. Continuous — действие в процессе. Perfect — действие уже произошло и важно для настоящего, прошлого или будущего результата.",
                Formula = "Simple: subject + verb\nContinuous: be + verb + ing\nPerfect: have / has / had + V3",
                Examples = "I read books. — Я читаю книги вообще.\nI am reading now. — Я читаю сейчас.\nI have read this book. — Я уже прочитал эту книгу.\n\nShe worked yesterday. — Она работала вчера.\nShe was working at 5. — Она работала в 5.\nShe had worked before dinner. — Она уже поработала до ужина.",
                CommonMistakes = "Нельзя смешивать форму времени без причины.\nНеправильно: I am read.\nПравильно: I am reading.\n\nНеправильно: I have readed.\nПравильно: I have read.",
                PracticeQuestion = "Какая форма показывает процесс: Simple, Continuous или Perfect?",
                PracticeAnswer = "Continuous",
                PracticeExplanation = "Continuous показывает действие в процессе."
            },

            new GrammarRule
            {
                Title = "Present Simple",
                Category = "Времена",
                Level = "A1",
                ShortExplanation = "Present Simple используется для обычных действий, привычек и фактов.",
                WhenToUse = "Используй Present Simple со словами usually, often, every day, always.",
                Formula = "I / you / we / they + verb\nhe / she / it + verb + s",
                Examples = "I like apples. — Я люблю яблоки.\nWe go to school. — Мы ходим в школу.\nHe likes milk. — Он любит молоко.\nShe reads books. — Она читает книги.",
                CommonMistakes = "Неправильно: He like milk.\nПравильно: He likes milk.\n\nС he / she / it обычно добавляем -s.",
                PracticeQuestion = "He ___ milk.",
                PracticeAnswer = "likes",
                PracticeExplanation = "С he в Present Simple добавляем -s: likes."
            },

            new GrammarRule
            {
                Title = "Present Continuous",
                Category = "Времена",
                Level = "A1",
                ShortExplanation = "Present Continuous показывает действие, которое происходит сейчас.",
                WhenToUse = "Используй его со словами now, at the moment, today, если действие происходит прямо сейчас.",
                Formula = "am / is / are + verb + ing",
                Examples = "I am reading now. — Я сейчас читаю.\nShe is drinking water. — Она сейчас пьёт воду.\nThey are playing football. — Они сейчас играют в футбол.",
                CommonMistakes = "Неправильно: She reading now.\nПравильно: She is reading now.\n\nНужен am / is / are.",
                PracticeQuestion = "She ___ reading now.",
                PracticeAnswer = "is",
                PracticeExplanation = "She = она, поэтому нужен is."
            },

            new GrammarRule
            {
                Title = "Present Perfect",
                Category = "Времена",
                Level = "A2",
                ShortExplanation = "Present Perfect показывает результат к настоящему моменту или жизненный опыт.",
                WhenToUse = "Используй Present Perfect, когда важно, что действие уже произошло, а точное время не главное.",
                Formula = "have / has + V3",
                Examples = "I have finished my homework. — Я закончил домашнюю работу.\nShe has visited London. — Она была в Лондоне.\nWe have seen this film. — Мы видели этот фильм.",
                CommonMistakes = "Неправильно: She have finished.\nПравильно: She has finished.\n\nНеправильно: I have saw.\nПравильно: I have seen.",
                PracticeQuestion = "She ___ finished her homework.",
                PracticeAnswer = "has",
                PracticeExplanation = "She = она, поэтому используется has."
            },

            new GrammarRule
            {
                Title = "Past Simple",
                Category = "Времена",
                Level = "A1",
                ShortExplanation = "Past Simple показывает действие, которое произошло в прошлом.",
                WhenToUse = "Используй Past Simple со словами yesterday, last week, last year, ago.",
                Formula = "regular verb + ed\nirregular verb — особая форма",
                Examples = "I played football yesterday. — Я играл в футбол вчера.\nShe watched TV. — Она смотрела телевизор.\nWe went to school. — Мы ходили в школу.\nHe had a dog. — У него была собака.",
                CommonMistakes = "Неправильно: I play football yesterday.\nПравильно: I played football yesterday.\n\nНеправильно: I goed to school.\nПравильно: I went to school.",
                PracticeQuestion = "Yesterday I ___ football.",
                PracticeAnswer = "played",
                PracticeExplanation = "Yesterday показывает прошлое, поэтому play → played."
            },

            new GrammarRule
            {
                Title = "Past Continuous",
                Category = "Времена",
                Level = "A2",
                ShortExplanation = "Past Continuous показывает процесс в прошлом.",
                WhenToUse = "Используй Past Continuous, когда действие происходило в определённый момент в прошлом.",
                Formula = "was / were + verb + ing",
                Examples = "I was reading at 5 o'clock. — Я читал в 5 часов.\nShe was sleeping. — Она спала.\nThey were playing football. — Они играли в футбол.",
                CommonMistakes = "Неправильно: I was read.\nПравильно: I was reading.\n\nПосле was / were нужен глагол с -ing.",
                PracticeQuestion = "They ___ playing football.",
                PracticeAnswer = "were",
                PracticeExplanation = "They = они, поэтому were."
            },

            new GrammarRule
            {
                Title = "Past Perfect",
                Category = "Времена",
                Level = "A2",
                ShortExplanation = "Past Perfect показывает действие, которое произошло раньше другого действия в прошлом.",
                WhenToUse = "Используй Past Perfect, когда нужно показать, что одно прошлое действие произошло до другого.",
                Formula = "had + V3",
                Examples = "I had finished my homework before dinner. — Я закончил домашнюю работу до ужина.\nShe had left before I came. — Она ушла до того, как я пришёл.",
                CommonMistakes = "Неправильно: I had went.\nПравильно: I had gone.\n\nПосле had нужна третья форма глагола.",
                PracticeQuestion = "I had ___ my homework.",
                PracticeAnswer = "finished",
                PracticeExplanation = "Finished — третья форма правильного глагола finish."
            },

            new GrammarRule
            {
                Title = "Future Simple",
                Category = "Времена",
                Level = "A1",
                ShortExplanation = "Future Simple показывает действие в будущем.",
                WhenToUse = "Используй will со словами tomorrow, next week, soon.",
                Formula = "subject + will + verb",
                Examples = "I will help you. — Я помогу тебе.\nShe will go to school tomorrow. — Она пойдёт в школу завтра.\nThey will play football. — Они будут играть в футбол.",
                CommonMistakes = "Неправильно: She will goes.\nПравильно: She will go.\n\nПосле will глагол без -s.",
                PracticeQuestion = "Tomorrow I ___ help you.",
                PracticeAnswer = "will",
                PracticeExplanation = "Tomorrow показывает будущее, поэтому нужен will."
            },

            new GrammarRule
            {
                Title = "Future Continuous",
                Category = "Времена",
                Level = "A2",
                ShortExplanation = "Future Continuous показывает процесс в будущем.",
                WhenToUse = "Используй его, когда действие будет происходить в определённый момент в будущем.",
                Formula = "will be + verb + ing",
                Examples = "I will be studying at 6. — Я буду заниматься в 6.\nShe will be working tomorrow morning. — Она будет работать завтра утром.",
                CommonMistakes = "Неправильно: I will studying.\nПравильно: I will be studying.\n\nНужна форма will be + ing.",
                PracticeQuestion = "I will ___ studying at 6.",
                PracticeAnswer = "be",
                PracticeExplanation = "Future Continuous: will be + verb + ing."
            },

            new GrammarRule
            {
                Title = "Future Perfect",
                Category = "Времена",
                Level = "A2",
                ShortExplanation = "Future Perfect показывает действие, которое завершится к определённому моменту в будущем.",
                WhenToUse = "Используй его со словами by tomorrow, by 6 o'clock, by next week.",
                Formula = "will have + V3",
                Examples = "I will have finished by 6. — Я закончу к 6.\nShe will have left by tomorrow. — Она уедет к завтрашнему дню.",
                CommonMistakes = "Неправильно: I will have finish.\nПравильно: I will have finished.\n\nПосле will have нужна третья форма глагола.",
                PracticeQuestion = "I will have ___ by 6.",
                PracticeAnswer = "finished",
                PracticeExplanation = "Future Perfect: will have + V3."
            },

            // =====================================================
            // VERBS AND IRREGULAR VERBS
            // =====================================================

            new GrammarRule
            {
                Title = "Глаголы",
                Category = "Глаголы",
                Level = "A1",
                ShortExplanation = "Глагол показывает действие или состояние.",
                WhenToUse = "Ищи глагол, когда нужно понять, что делает человек или предмет.",
                Formula = "go — идти\nread — читать\nplay — играть\nbe — быть\nhave — иметь",
                Examples = "I read books. — Я читаю книги.\nShe plays tennis. — Она играет в теннис.\nThey are happy. — Они счастливы.",
                CommonMistakes = "В английском предложении почти всегда нужен глагол.\nНеправильно: I happy.\nПравильно: I am happy.",
                PracticeQuestion = "Какая часть речи слово play?",
                PracticeAnswer = "verb",
                PracticeExplanation = "Play показывает действие, значит это verb."
            },

            new GrammarRule
            {
                Title = "Правильные глаголы",
                Category = "Глаголы",
                Level = "A1",
                ShortExplanation = "Правильные глаголы образуют Past Simple и V3 с помощью -ed.",
                WhenToUse = "Если глагол правильный, в прошлом времени и в Perfect чаще всего добавляется -ed.",
                Formula = "play — played — played\nwatch — watched — watched\nopen — opened — opened",
                Examples = "I played football. — Я играл в футбол.\nShe watched TV. — Она смотрела телевизор.\nI have opened the door. — Я открыл дверь.",
                CommonMistakes = "Неправильно: I play yesterday.\nПравильно: I played yesterday.",
                PracticeQuestion = "Past Simple от watch:",
                PracticeAnswer = "watched",
                PracticeExplanation = "Watch — правильный глагол, поэтому watched."
            },

            new GrammarRule
            {
                Title = "Неправильные глаголы: список исключений",
                Category = "Глаголы",
                Level = "A1-A2",
                ShortExplanation = "Неправильные глаголы не образуют Past Simple и V3 по обычному правилу -ed. Их нужно запоминать.",
                WhenToUse = "Эти формы используются в Past Simple и Perfect.",
                Formula =
                    "be — was/were — been\n" +
                    "become — became — become\n" +
                    "begin — began — begun\n" +
                    "break — broke — broken\n" +
                    "bring — brought — brought\n" +
                    "build — built — built\n" +
                    "buy — bought — bought\n" +
                    "catch — caught — caught\n" +
                    "choose — chose — chosen\n" +
                    "come — came — come\n" +
                    "cost — cost — cost\n" +
                    "cut — cut — cut\n" +
                    "do — did — done\n" +
                    "draw — drew — drawn\n" +
                    "drink — drank — drunk\n" +
                    "drive — drove — driven\n" +
                    "eat — ate — eaten\n" +
                    "fall — fell — fallen\n" +
                    "feel — felt — felt\n" +
                    "find — found — found\n" +
                    "fly — flew — flown\n" +
                    "forget — forgot — forgotten\n" +
                    "get — got — got\n" +
                    "give — gave — given\n" +
                    "go — went — gone\n" +
                    "grow — grew — grown\n" +
                    "have — had — had\n" +
                    "hear — heard — heard\n" +
                    "hold — held — held\n" +
                    "keep — kept — kept\n" +
                    "know — knew — known\n" +
                    "leave — left — left\n" +
                    "lose — lost — lost\n" +
                    "make — made — made\n" +
                    "meet — met — met\n" +
                    "pay — paid — paid\n" +
                    "put — put — put\n" +
                    "read — read — read\n" +
                    "ride — rode — ridden\n" +
                    "ring — rang — rung\n" +
                    "run — ran — run\n" +
                    "say — said — said\n" +
                    "see — saw — seen\n" +
                    "sell — sold — sold\n" +
                    "send — sent — sent\n" +
                    "sing — sang — sung\n" +
                    "sit — sat — sat\n" +
                    "sleep — slept — slept\n" +
                    "speak — spoke — spoken\n" +
                    "spend — spent — spent\n" +
                    "stand — stood — stood\n" +
                    "swim — swam — swum\n" +
                    "take — took — taken\n" +
                    "teach — taught — taught\n" +
                    "tell — told — told\n" +
                    "think — thought — thought\n" +
                    "understand — understood — understood\n" +
                    "wear — wore — worn\n" +
                    "win — won — won\n" +
                    "write — wrote — written",
                Examples = "I went to school. — Я пошёл в школу.\nI have gone home. — Я ушёл домой.\nShe saw a dog. — Она увидела собаку.\nShe has seen this film. — Она видела этот фильм.",
                CommonMistakes = "Неправильно: I goed.\nПравильно: I went.\n\nНеправильно: I have saw.\nПравильно: I have seen.",
                PracticeQuestion = "Past Simple от go:",
                PracticeAnswer = "went",
                PracticeExplanation = "Go — went — gone."
            },

            // =====================================================
            // MODAL VERBS
            // =====================================================

            new GrammarRule
            {
                Title = "Модальные глаголы",
                Category = "Модальные глаголы",
                Level = "A1-A2",
                ShortExplanation = "Модальные глаголы помогают говорить о способности, разрешении, обязанности, совете и возможности.",
                WhenToUse = "После модального глагола основной глагол идёт в простой форме без -s, -ed, to.",
                Formula = "can + verb\nmust + verb\nshould + verb\nmay + verb\nmight + verb",
                Examples = "I can swim. — Я умею плавать.\nYou must listen. — Ты должен слушать.\nYou should sleep. — Тебе следует поспать.\nIt may rain. — Возможно, будет дождь.",
                CommonMistakes = "Неправильно: He can swims.\nПравильно: He can swim.\n\nНеправильно: She must goes.\nПравильно: She must go.",
                PracticeQuestion = "He can ___",
                PracticeAnswer = "swim",
                PracticeExplanation = "После can глагол идёт в простой форме: swim."
            },

            new GrammarRule
            {
                Title = "Can / Can't",
                Category = "Модальные глаголы",
                Level = "A1",
                ShortExplanation = "Can означает “мочь” или “уметь”. Can't означает “не мочь” или “не уметь”.",
                WhenToUse = "Используй can для способности или возможности.",
                Formula = "subject + can + verb\nsubject + can't + verb",
                Examples = "I can swim. — Я умею плавать.\nBirds can fly. — Птицы умеют летать.\nFish can't walk. — Рыбы не умеют ходить.",
                CommonMistakes = "Неправильно: He can swims.\nПравильно: He can swim.",
                PracticeQuestion = "Birds ___ fly.",
                PracticeAnswer = "can",
                PracticeExplanation = "Птицы умеют летать, поэтому can."
            },

            new GrammarRule
            {
                Title = "Must / Mustn't",
                Category = "Модальные глаголы",
                Level = "A1",
                ShortExplanation = "Must означает “должен”. Mustn't означает “нельзя”.",
                WhenToUse = "Используй must для сильной обязанности или правила.",
                Formula = "subject + must + verb\nsubject + mustn't + verb",
                Examples = "You must do your homework. — Ты должен сделать домашнюю работу.\nYou mustn't run here. — Здесь нельзя бегать.",
                CommonMistakes = "Неправильно: He musts go.\nПравильно: He must go.",
                PracticeQuestion = "You ___ do your homework.",
                PracticeAnswer = "must",
                PracticeExplanation = "Must показывает обязанность."
            },

            new GrammarRule
            {
                Title = "Should / Shouldn't",
                Category = "Модальные глаголы",
                Level = "A1-A2",
                ShortExplanation = "Should означает “следует”. Shouldn't означает “не следует”.",
                WhenToUse = "Используй should для совета.",
                Formula = "subject + should + verb\nsubject + shouldn't + verb",
                Examples = "You should drink water. — Тебе следует пить воду.\nYou shouldn't eat too much sugar. — Тебе не следует есть слишком много сахара.",
                CommonMistakes = "Неправильно: She should drinks.\nПравильно: She should drink.",
                PracticeQuestion = "You ___ sleep more.",
                PracticeAnswer = "should",
                PracticeExplanation = "Should используется для совета."
            },

            new GrammarRule
            {
                Title = "May / Might",
                Category = "Модальные глаголы",
                Level = "A2",
                ShortExplanation = "May и might показывают возможность.",
                WhenToUse = "Используй may или might, когда что-то возможно, но не точно.",
                Formula = "subject + may + verb\nsubject + might + verb",
                Examples = "It may rain. — Возможно, будет дождь.\nShe might come. — Возможно, она придёт.",
                CommonMistakes = "Неправильно: She might comes.\nПравильно: She might come.",
                PracticeQuestion = "It ___ rain tomorrow.",
                PracticeAnswer = "may",
                PracticeExplanation = "May показывает возможность."
            },

            // =====================================================
            // NOUNS AND EXCEPTIONS
            // =====================================================

            new GrammarRule
            {
                Title = "Существительные",
                Category = "Существительные",
                Level = "A1",
                ShortExplanation = "Существительное называет предмет, человека, место, животное или идею.",
                WhenToUse = "Существительные отвечают на вопросы: кто? что?",
                Formula = "person: teacher, doctor\nplace: school, park\nthing: book, table\nanimal: cat, dog\nidea: love, time",
                Examples = "book — книга\nteacher — учитель\nschool — школа\nfamily — семья\ncat — кошка",
                CommonMistakes = "В английском перед обычным исчисляемым существительным в единственном числе часто нужен артикль: a book, the book.",
                PracticeQuestion = "Что из этого существительное: quickly / book / green?",
                PracticeAnswer = "book",
                PracticeExplanation = "Book — это предмет, значит существительное."
            },

            new GrammarRule
            {
                Title = "Множественное число: обычные правила",
                Category = "Существительные",
                Level = "A1",
                ShortExplanation = "Чтобы сказать, что предметов несколько, часто добавляется -s, -es или -ies.",
                WhenToUse = "Добавляй -s обычно, -es после s, x, ch, sh, а -ies после согласной + y.",
                Formula = "cat → cats\nbox → boxes\nwatch → watches\nbaby → babies",
                Examples = "one cat — two cats\none box — two boxes\none watch — two watches\none baby — two babies",
                CommonMistakes = "Неправильно: two boxs\nПравильно: two boxes\n\nНеправильно: two babys\nПравильно: two babies",
                PracticeQuestion = "two ___",
                PracticeAnswer = "boxes",
                PracticeExplanation = "Box заканчивается на x, поэтому boxes."
            },

            new GrammarRule
            {
                Title = "Множественное число: исключения",
                Category = "Существительные",
                Level = "A1-A2",
                ShortExplanation = "Некоторые существительные образуют множественное число не по обычным правилам.",
                WhenToUse = "Эти формы нужно запоминать.",
                Formula =
                    "child — children\n" +
                    "person — people\n" +
                    "man — men\n" +
                    "woman — women\n" +
                    "tooth — teeth\n" +
                    "foot — feet\n" +
                    "mouse — mice\n" +
                    "goose — geese\n" +
                    "ox — oxen\n" +
                    "sheep — sheep\n" +
                    "fish — fish\n" +
                    "deer — deer\n" +
                    "series — series\n" +
                    "species — species",
                Examples = "one child — two children\none person — two people\none tooth — two teeth\none sheep — two sheep",
                CommonMistakes = "Неправильно: childs\nПравильно: children\n\nНеправильно: sheeps\nПравильно: sheep",
                PracticeQuestion = "one child — two ___",
                PracticeAnswer = "children",
                PracticeExplanation = "Child имеет неправильную форму: children."
            },

            new GrammarRule
            {
                Title = "Существительные на -f / -fe",
                Category = "Существительные",
                Level = "A2",
                ShortExplanation = "Некоторые существительные на -f или -fe во множественном числе меняют f на ves.",
                WhenToUse = "Эти слова лучше запоминать как исключения.",
                Formula =
                    "leaf — leaves\n" +
                    "knife — knives\n" +
                    "life — lives\n" +
                    "wife — wives\n" +
                    "wolf — wolves\n" +
                    "shelf — shelves\n" +
                    "thief — thieves\n" +
                    "half — halves",
                Examples = "one leaf — two leaves\none knife — two knives\none wolf — two wolves",
                CommonMistakes = "Неправильно: leafs\nПравильно: leaves\n\nНеправильно: knifes\nПравильно: knives",
                PracticeQuestion = "one leaf — two ___",
                PracticeAnswer = "leaves",
                PracticeExplanation = "Leaf → leaves."
            },

            new GrammarRule
            {
                Title = "Исчисляемые и неисчисляемые существительные",
                Category = "Существительные",
                Level = "A1",
                ShortExplanation = "Исчисляемые можно посчитать. Неисчисляемые обычно нельзя считать по одному.",
                WhenToUse = "Many используется с исчисляемыми. Much используется с неисчисляемыми.",
                Formula = "many books\nmany apples\nmuch water\nmuch milk",
                Examples = "I have many books. — У меня много книг.\nI drink much water. — Я пью много воды.",
                CommonMistakes = "Неправильно: many water\nПравильно: much water",
                PracticeQuestion = "___ water",
                PracticeAnswer = "much",
                PracticeExplanation = "Water неисчисляемое, поэтому much water."
            },

            new GrammarRule
            {
                Title = "Притяжательный 's",
                Category = "Существительные",
                Level = "A1",
                ShortExplanation = "'s показывает, что что-то кому-то принадлежит.",
                WhenToUse = "Используй 's после имени или существительного.",
                Formula = "Tom's book\nmother's bag\nteacher's desk",
                Examples = "Tom's book — книга Тома\nAnna's cat — кошка Анны\nMy mother's bag — сумка моей мамы",
                CommonMistakes = "Неправильно: Tom book\nПравильно: Tom's book",
                PracticeQuestion = "Книга Тома:",
                PracticeAnswer = "Tom's book",
                PracticeExplanation = "Принадлежность: Tom's book."
            },

            // =====================================================
            // PARTS OF SPEECH
            // =====================================================

            new GrammarRule
            {
                Title = "Части речи",
                Category = "Части речи",
                Level = "A1",
                ShortExplanation = "Части речи помогают понять, какую роль слово играет в предложении.",
                WhenToUse = "Сначала определи, слово называет предмет, действие, признак, место, время или связь.",
                Formula = "noun — предмет / человек / место\nverb — действие\nadjective — признак\nadverb — как происходит действие\npronoun — заменяет существительное\npreposition — связь слов\narticle — a / an / the\nconjunction — соединяет слова",
                Examples = "book — noun\nrun — verb\nred — adjective\nquickly — adverb\nhe — pronoun\nin — preposition\nand — conjunction",
                CommonMistakes = "Не каждое слово переводится отдельно. Важно видеть его роль в предложении.",
                PracticeQuestion = "Какая часть речи слово run?",
                PracticeAnswer = "verb",
                PracticeExplanation = "Run показывает действие, значит это verb."
            },

            new GrammarRule
            {
                Title = "Noun — существительное",
                Category = "Части речи",
                Level = "A1",
                ShortExplanation = "Noun называет предмет, человека, место, животное или идею.",
                WhenToUse = "Используй существительные как главные слова: кто? что?",
                Formula = "a book\nthe teacher\nmy family",
                Examples = "I have a book. — У меня есть книга.\nThe teacher is here. — Учитель здесь.",
                CommonMistakes = "В единственном числе часто нужен артикль: a book, an apple.",
                PracticeQuestion = "Book — это...",
                PracticeAnswer = "noun",
                PracticeExplanation = "Book — предмет, значит noun."
            },

            new GrammarRule
            {
                Title = "Verb — глагол",
                Category = "Части речи",
                Level = "A1",
                ShortExplanation = "Verb показывает действие или состояние.",
                WhenToUse = "Ищи verb, чтобы понять, что происходит.",
                Formula = "run, read, eat, be, have",
                Examples = "I read. — Я читаю.\nShe eats. — Она ест.\nThey are happy. — Они счастливы.",
                CommonMistakes = "В английском предложении обычно нужен глагол.",
                PracticeQuestion = "Eat — это...",
                PracticeAnswer = "verb",
                PracticeExplanation = "Eat показывает действие."
            },

            new GrammarRule
            {
                Title = "Adjective — прилагательное",
                Category = "Части речи",
                Level = "A1",
                ShortExplanation = "Adjective описывает предмет или человека.",
                WhenToUse = "Прилагательное часто стоит перед существительным.",
                Formula = "adjective + noun",
                Examples = "a red apple — красное яблоко\na big house — большой дом\na good book — хорошая книга",
                CommonMistakes = "Прилагательное в английском обычно не меняется по числу: red apple, red apples.",
                PracticeQuestion = "В фразе a red apple слово red — это...",
                PracticeAnswer = "adjective",
                PracticeExplanation = "Red описывает apple."
            },

            new GrammarRule
            {
                Title = "Adverb — наречие",
                Category = "Части речи",
                Level = "A1-A2",
                ShortExplanation = "Adverb показывает, как, когда или где происходит действие.",
                WhenToUse = "Наречия часто отвечают на вопросы: как? когда? где?",
                Formula = "quickly, slowly, often, usually, here, there",
                Examples = "She runs quickly. — Она быстро бегает.\nI usually drink tea. — Я обычно пью чай.\nHe is here. — Он здесь.",
                CommonMistakes = "Adjective описывает noun, а adverb описывает verb.\nGood boy — adjective.\nHe plays well — adverb.",
                PracticeQuestion = "Quickly — это...",
                PracticeAnswer = "adverb",
                PracticeExplanation = "Quickly показывает, как происходит действие."
            },

            new GrammarRule
            {
                Title = "Pronoun — местоимение",
                Category = "Части речи",
                Level = "A1",
                ShortExplanation = "Pronoun заменяет существительное.",
                WhenToUse = "Используй местоимение, чтобы не повторять имя или предмет.",
                Formula = "I, you, he, she, it, we, they",
                Examples = "Tom is here. He is happy.\nAnna has a cat. She likes it.",
                CommonMistakes = "He — он. She — она. It — оно / это для предметов и животных, если пол не важен.",
                PracticeQuestion = "He — это...",
                PracticeAnswer = "pronoun",
                PracticeExplanation = "He заменяет имя мужчины или мальчика."
            },

            new GrammarRule
            {
                Title = "Preposition — предлог",
                Category = "Части речи",
                Level = "A1",
                ShortExplanation = "Preposition показывает связь между словами.",
                WhenToUse = "Используй предлоги для места, времени и направления.",
                Formula = "in the room\non the table\nat school\nto the park",
                Examples = "The book is on the table. — Книга на столе.\nI am in the room. — Я в комнате.\nI go to school. — Я иду в школу.",
                CommonMistakes = "Русский предлог не всегда переводится одним и тем же английским предлогом.",
                PracticeQuestion = "The book is ___ the table.",
                PracticeAnswer = "on",
                PracticeExplanation = "На поверхности стола = on the table."
            },

            new GrammarRule
            {
                Title = "Article — артикль",
                Category = "Части речи",
                Level = "A1",
                ShortExplanation = "Article помогает понять, говорим ли мы о любом предмете или о конкретном.",
                WhenToUse = "A / an — один любой предмет. The — конкретный предмет.",
                Formula = "a dog\nan apple\nthe door",
                Examples = "I have a dog. — У меня есть собака.\nShe has an apple. — У неё есть яблоко.\nOpen the door. — Открой дверь.",
                CommonMistakes = "An используется перед гласным звуком: an apple, an egg.",
                PracticeQuestion = "She has ___ apple.",
                PracticeAnswer = "an",
                PracticeExplanation = "Apple начинается с гласного звука."
            },

            new GrammarRule
            {
                Title = "Conjunction — союз",
                Category = "Части речи",
                Level = "A1",
                ShortExplanation = "Conjunction соединяет слова или части предложения.",
                WhenToUse = "Используй союзы, чтобы соединять идеи.",
                Formula = "and, but, or, because",
                Examples = "I have a book and a pen. — У меня есть книга и ручка.\nI like tea, but I don't like coffee. — Я люблю чай, но не люблю кофе.",
                CommonMistakes = "But показывает противопоставление, and просто соединяет.",
                PracticeQuestion = "I like apples ___ bananas.",
                PracticeAnswer = "and",
                PracticeExplanation = "And соединяет два слова."
            }
        };

        public static List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            categories.Add("Все правила");

            categories.AddRange(
                Rules
                    .Select(r => r.Category)
                    .Distinct()
                    .OrderBy(c => c));

            return categories;
        }

        public static List<GrammarRule> GetRules(string category)
        {
            if (string.IsNullOrWhiteSpace(category) || category == "Все правила")
                return Rules.ToList();

            return Rules
                .Where(r => r.Category == category)
                .ToList();
        }

        public static List<GrammarRule> GetAllRules()
        {
            return Rules.ToList();
        }
    }
}