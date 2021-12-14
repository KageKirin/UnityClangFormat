
## publishing
publish:
	npm publish --access public

## formatting makerules
ALL_SOURCE_FILES := \
	$(shell fd ".*\.h"   -- Assets/Scripts/Editor)  \
	$(shell fd ".*\.c"   -- Assets/Scripts/Editor)  \
	$(shell fd ".*\.cpp" -- Assets/Scripts/Editor)

ALL_TRACKED_FILES := \
	$(shell git ls-files -- Assets/Scripts/Editor | rg ".*\.h")    \
	$(shell git ls-files -- Assets/Scripts/Editor | rg ".*\.c")    \
	$(shell git ls-files -- Assets/Scripts/Editor | rg ".*\.cpp")

ALL_MODIFIED_FILES := \
	$(shell git lsm -- Assets/Scripts/Editor)


format-all: $(ALL_SOURCE_FILES)
	clang-format -i $^

format: $(ALL_TRACKED_FILES)
	clang-format -i $^
	#$(GENIE) format

q qformat: $(ALL_MODIFIED_FILES)
	clang-format -i $^

