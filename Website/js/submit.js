// Add more models dynamically

var i = 0; // number of models

function fixi() {
	var input = document.getElementById("i");
	input.value = i;
}

function add() {
	var modelHtml = `
	<div class="model"  id="model_${i}">
		<h5> Модел ${i + 1} </h5>
		<div class="row">
			<div class="col">
				<label for="m_name${i}"> Име на модела </label>
				<input class="form-control"type="text" name="m_name${i}" maxlength="64" placeholder="Моят модел" aria-describedby="m_name${i}help" required>
				<small id="m_name${i}help" class="form-text text-muted">
                	Максимум 64 знака.
                </small>
			</div>
			<div class="col">
				<label for="m_desc${i}"> Описание на модела </label>
				<textarea class="form-control" type="text" name="m_desc${i}" rows="1" maxlength="256" aria-describedby="m_desc${i}help" required>Моделът показва ...</textarea>
				<small id="m_desc${i}help" class="form-text text-muted">
                	Максимум 256 знака.
                </small>
			</div>
			<div class="col">
				<div class="form-group">
					<label for="m_pic${i}"> Изображение </label>
					<input type="file" class="filebutton" name="m_pic${i}" accept="image/png, image/jpeg" aria-describedby="m_pic${i}help" required>
					<small id="m_pic${i}help" class="form-text text-muted">
                		Максимум 5MB.
                	</small>
				</div>
			</div>
			<div class="col">
				<div class="form-group">
					<label for="m_model${i}"> Модел </label>
					<input type="file" class="filebutton" name="m_model${i}" accept=".fbx, .prefab" aria-describedby="m_model${i}help required>
					<small id="m_model${i}help" class="form-text text-muted">
	                	Максимум 200MB.
	                </small>
				</div>
			</div>
			<div class="col">
				<div class="form-group">
					<label for="m_info${i}"> Информация </label>
					<input type="file" class="filebutton" name="m_info${i}" accept=".md" aria-describedby="m_info${i}help required>
					<small id="m_info${i}help" class="form-text text-muted">
	                	Максимум 1MB.
	                </small>
				</div>
			</div>
		</div>
	</div>
	<br>
`;
	var table = document.getElementById("add-here");
	table.innerHTML += modelHtml;
	i++;
	fixi();
}

function remove() {
	i--;
	var element = document.getElementById(`model_${i}`);
	element.parentNode.removeChild(element);
	fixi();
}