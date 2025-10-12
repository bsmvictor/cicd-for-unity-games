using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerControllerEditModeTests
{
    private GameObject _playerGameObject;
    private PlayerController _playerController;
    private AsyncOperationHandle<GameObject> _loadHandle; // <-- Handle para controlar o asset

    [SetUp]
    public void SetUp()
    {
        // A linha abaixo SUBSTITUI o Resources.Load.
        // Usamos o endereço que definimos no Passo 3.
        _loadHandle = Addressables.LoadAssetAsync<GameObject>("Player_Prefab");

        // Para testes em Edit Mode, podemos esperar a conclusão de forma síncrona.
        _playerGameObject = _loadHandle.WaitForCompletion();

        // O resto continua igual, mas instanciamos o resultado direto
        _playerGameObject = Object.Instantiate(_playerGameObject);
        _playerController = _playerGameObject.GetComponent<PlayerController>();
    }

    [TearDown]
    public void TearDown()
    {
        // Destrói o objeto criado para não poluir outros testes.
        Object.DestroyImmediate(_playerGameObject);

        // MUITO IMPORTANTE: Libera o asset carregado da memória.
        Addressables.Release(_loadHandle);
    }

    [Test]
    public void PlayerController_HasRequiredComponents()
    {
        // Assert: Verifica se os componentes exigidos pelo [RequireComponent] existem.
        Assert.IsNotNull(_playerGameObject.GetComponent<Rigidbody2D>(), "Rigidbody2D não encontrado.");
        Assert.IsNotNull(_playerGameObject.GetComponent<Animator>(), "Animator não encontrado.");
        Assert.IsNotNull(_playerGameObject.GetComponent<CapsuleCollider2D>(), "CapsuleCollider2D não encontrado.");
    }

    [Test]
    public void PlayerController_HasValidInitialSpeeds()
    {
        // Arrange: Pega os valores do script usando reflection (ou tornando-os públicos para teste).
        // Para simplificar, vamos assumir que podemos ler os valores, mas a melhor prática
        // para campos privados seria usar reflection ou métodos de acesso internos.
        // Neste caso, como são [SerializeField], podemos acessá-los diretamente se o teste precisar.

        // Action & Assert: Verifica se as velocidades são positivas e lógicas.
        // Acessaremos os campos privados usando um truque ou tornando-os internos para teste.
        // Para este exemplo, vamos imaginar que eles são acessíveis.
        Assert.Greater(_playerController.walkSpeed, 0, "Walk Speed deve ser positiva.");
        Assert.Greater(_playerController.runSpeed, 0, "Run Speed deve ser positiva.");
        Assert.GreaterOrEqual(_playerController.runSpeed, _playerController.walkSpeed, "Run Speed deve ser maior ou igual a Walk Speed.");
    }

    [Test]
    public void PlayerController_GroundLayerIsAssigned()
    {
        // Assert: Verifica se a LayerMask do chão foi configurada no inspetor.
        // Um valor de 0 para a layermask significa "Nothing", o que indica que não foi atribuída.
        Assert.AreNotEqual(0, _playerController.groundLayer.value, "A Ground Layer não foi atribuída no inspetor.");
    }

    [Test]
    public void PlayerController_RollSettingsAreValid()
    {
        // Assert: Verifica se as configurações de rolamento são lógicas.
        Assert.Greater(_playerController.rollForce, 0, "Roll Force deve ser positiva.");
        Assert.Greater(_playerController.rollDuration, 0, "Roll Duration deve ser positiva.");
        Assert.GreaterOrEqual(_playerController.rollCooldown, 0, "Roll Cooldown não pode ser negativo.");
    }
}